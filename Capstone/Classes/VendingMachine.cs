using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        /*takes in inventory file, returns list of objects of type
         * <VendingMachineItem> so they can be operated on and displayed
         */
        public List<VendingMachineItem> ReadFile()
        {
            //enter your file name here if it needs to be changed
            string basePath = Environment.CurrentDirectory;
            string specificPath = @"vendingmachine.csv";
            string fullPath = Path.Combine(basePath, specificPath);
            List<VendingMachineItem> listOfItems = new List<VendingMachineItem>();

            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        List<string> temp = new List<string>();
                        temp.AddRange(line.Split('|').ToList());
                        VendingMachineItem obj = new VendingMachineItem(temp[0], temp[1], double.Parse(temp[2]), temp[3]);
                        listOfItems.Add(obj);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("File path broken");
                Console.WriteLine(ex);
            }
            return listOfItems;
        }

        //prints table of items
        public void DisplayItems(List<VendingMachineItem> masterList)
        {
            Console.WriteLine("");
            Console.WriteLine("Slot       Name       Price       Stock");
            Console.WriteLine("---------------------------------------");
            foreach (VendingMachineItem v in masterList)
            {
                string stock = getStock(v.Stock);
                Console.WriteLine($"{v.SlotNumber}  {v.Name}  ${v.Price}  {stock}");
            }
        }

        //checks stock, assures that if it is sold out or not
        string getStock(int integerStock)
        {
            if (integerStock <= 0)
            {
                return "Sold Out";
            }
            else
            {
                return integerStock.ToString();
            }
        }

        public void PurchaseInit(List<VendingMachineItem> masterList)
        {
            double userBalance = 0.00;
            bool done = false;
            while (!done)
            {
                Console.WriteLine("\n(1) Feed Money \n(2) Select Product \n(3) Finish Transaction\n");
                int selection = int.Parse(Console.ReadLine());
                if (selection == 1)
                {
                    userBalance += FeedMoney();
                    Console.WriteLine($"Current Money Provided: ${userBalance}");
                }
                else if (selection == 2)
                {
                    userBalance = SelectProduct(masterList, userBalance);
                }
                else if (selection == 3)
                {
                    done = true;
                }
                else Console.WriteLine("Please enter (1), (2), or (3).");
            }
        }

        public double FeedMoney()
        {
            List<double> possibleValues = new List<double> { 1.00, 2.00, 5.00, 10.00 };
            bool done = false;
            double runningTotal = 0.00;
            while (!done)
            {
                Console.WriteLine("Please provide a whole dollar amount in the format X.XX\nEnter X to Exit\n");
                string temp = Console.ReadLine();
                if (double.TryParse(temp, out double num))
                {
                    double amountToAdd = double.Parse(temp);
                    if (possibleValues.Contains(amountToAdd))
                    {
                        Console.WriteLine($"Added ${amountToAdd}\n");
                        runningTotal += amountToAdd;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid dollar amount\n");
                    }
                }
                else if(temp == "X")
                {
                    Console.WriteLine("Exiting\n");
                    done = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid whole dollar amount\n");
                }
            }
            return runningTotal;

        }

        public double SelectProduct(List<VendingMachineItem> masterList, double userBalance)
        {
            DisplayItems(masterList);
            Console.WriteLine("\nPlease enter a product code\n");
            string userChoice = Console.ReadLine();
            foreach (VendingMachineItem v in masterList)
            {
                if (userChoice.Equals(v.SlotNumber) && v.Stock > 0)
                {
                    if (userBalance > v.Price)
                    {
                        v.Stock--;
                        userBalance -= v.Price;
                        Console.WriteLine($"{v.Name}\nRemaining balance: {userBalance}\n{v.Message}\n");
                        return userBalance;
                    }
                    else
                    {
                        Console.WriteLine("Insufficent funds");
                        return userBalance;
                    }
                }
            }
            Console.WriteLine("Invalid Selection");
            return userBalance;
        }
    }
}

    
    

