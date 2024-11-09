using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College
{
    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;

        public Student(int id, string name, string dob, string ph, string email)
        {
            Id = id;
            Name = name;
            DateOfBirth = dob;
            PhoneNumber = ph;
            Email = email;
        }

        public void DisplayId()
        {
            Console.WriteLine($"Id: {Id} \t Name: {Name}");
            Console.WriteLine($"Date of Birth: {DateOfBirth}");
            Console.WriteLine($"Phone: {PhoneNumber}");
            Console.WriteLine($"Email: {Email} \n");
        }
    }
}
