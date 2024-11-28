using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeExpenseTrackingApp
{
    internal class Employee
    {
        public Employee()
        {
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public double Salary { get; set; }

        public Employee(int id, string name, string designation, double salary)
        {
            Id = id;
            Name = name;
            Designation = designation;
            Salary = salary;
        }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Designation: {Designation}, Salary: {Salary}";
        }

    }
}
