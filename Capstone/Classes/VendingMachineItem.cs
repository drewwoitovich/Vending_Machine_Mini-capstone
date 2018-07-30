using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class VendingMachineItem
    {
        public string SlotNumber { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public int Stock { get; set; }
        public string Message;
        

        public VendingMachineItem(string slotNumber, string name, double price, string type)
        {
            SlotNumber = slotNumber;
            Name = name;
            Price = price;
            Type = type;
            Stock = 5;
            Message = (Type == "Chip") ? "Crunch Crunch, Yum!" :
                (Type == "Candy") ? "Munch Munch Yum!" :
                (Type == "Drink") ? "Glug Glug, Yum!" : "Chew Chew, Yum!";
        }

    }
}
