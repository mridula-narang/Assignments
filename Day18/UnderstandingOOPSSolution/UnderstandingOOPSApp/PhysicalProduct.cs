using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingOOPSApp
{
    internal class PhysicalProduct : Product
    {
        public string ShippingAddress { get; set; } = string.Empty;
        public PhysicalProduct(string name, double price, string shippingAddress)
        {
            Name = name;
            Price = price;
            ShippingAddress = shippingAddress;
        }
        public override string DeliveryMethod() //implementation of the abstract method
        {
            return "Delivered to the shipping address";
        }
    }
}
