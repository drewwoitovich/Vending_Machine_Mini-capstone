using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class UserInterface
    {
        private VendingMachine vendingMachine;

        public UserInterface(VendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine;
        }

        public void RunInterface()
        {
            bool done = false;
            List<VendingMachineItem> masterList = vendingMachine.ReadFile();
            while (!done)
            {
                Console.WriteLine("(1) Display vending machine items \n(2) Purchase \n(3) Exit\n");
                int result = GetSelection();
                if (result == 1)
                {
                    vendingMachine.DisplayItems(masterList);
                }
                else if (result == 2)
                {
                    //int resultPurchase = GetSelection();
                    vendingMachine.PurchaseInit(masterList);
                }
                else if (result == 3)
                {
                    Console.WriteLine("Exiting...");
                    done = true;
                }
                else Console.WriteLine("Please enter (1), (2), or (3).");
            }
        }
        public static int GetSelection()
        {
            while (true)
            {
                string temp = Console.ReadLine();
                int num;
                if (Int32.TryParse(temp, out num))
                {
                    return int.Parse(temp);
                }
                else
                {
                    Console.WriteLine("Please enter a valid selection.\n");
                }
            }
         
        }
    }
}
