﻿namespace EFWebApiApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string BasicImage { get; set; } = string.Empty;
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public ICollection<ProductImage> ProductImages { get; internal set; }

        public Product()
        {
            OrderDetails = new List<OrderDetail>();
        }
    }
}
