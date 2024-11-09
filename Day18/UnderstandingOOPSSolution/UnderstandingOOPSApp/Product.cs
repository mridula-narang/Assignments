using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingOOPSApp
{
    //Product class to understand abstraction and its needs
    //Product class is an abstract class. We will use this class to create concrete classes.
    public abstract class Product
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }

        public abstract string DeliveryMethod(); //abstract method. describes the delivery method of the product

        public void DisplayProductInfo() //concrete method. displays the product information
        {
            Console.WriteLine($"Product: {Name}, Price: ${Price}");
        }
    }
}
