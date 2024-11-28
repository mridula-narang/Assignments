﻿namespace PizzaStoreAPI.Models
{
    public class Customer : IEquatable<Customer>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }

        public bool Equals(Customer? other)
        {
            return (this ?? new Customer()).Id == (other ?? new Customer()).Id;
        }
    }
}
