using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingOOPSApp
{
    internal class DigitalProduct : Product
    {
        public string FileFormat { get; set; }

        public DigitalProduct(string name, double price, string fileFormat)
        {
            Name = name;
            Price = price;
            FileFormat = fileFormat;
        }

        public override string DeliveryMethod() // implementation of the abstract method
        {
            return "Downloadable via email or online link.";
        }
    }
}
