using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp
{
    internal class Customer
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public Customer()
        {
        }

        public Customer(string name, DateTime dateOfBirth, string gender)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }

        public virtual void CustomerDetails()
        {
            Console.WriteLine("Enter the customer name:");
            Name = Console.ReadLine() ?? "";
            Console.WriteLine("Enter the customer date of birth:");
            DateOfBirth = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter the gender(M/F");
            Gender = Console.ReadLine() ?? "F";

        }
    }
}
