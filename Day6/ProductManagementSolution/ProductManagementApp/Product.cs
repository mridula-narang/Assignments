using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementApp
{
    internal class Product
    {
        public Product()
        { 
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int SupplierId { get; set; }

        public Product(int id, string name, int price, int quantity, int supplierId)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
            SupplierId = supplierId;
        }
    }
}
