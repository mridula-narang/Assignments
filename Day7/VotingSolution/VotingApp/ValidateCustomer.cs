using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp
{
    internal class ValidateCustomer : IValidateCustomer
    {
        public int CalculateAge(DateTime dateOfBirth)
        {
            var customerAge = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now < dateOfBirth.AddYears(customerAge))
            {
                customerAge--;
            }
            return customerAge;
        }
        public void ValidateCustomerByAge(Customer customer)
        {
            var age = CalculateAge(customer.DateOfBirth);
            PrintResult(customer, age);
        }

        private void PrintResult(Customer customer, int age)
        {
            var salutation = customer.Gender == "M" ? "Mr" : "Ms";
            if (age < 18)
            {
                Console.WriteLine($"Dear {salutation}. {customer.Name} you are {age} years old and so not eligible to vote.");
            }
            else
            {
                Console.WriteLine($"Dear {salutation}. {customer.Name} you are {age} years old and so you are eligible to vote.");
            }
        }
    }
}
