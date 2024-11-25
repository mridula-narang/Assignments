using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTrackingApp
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
        public DateTime DateOfBirth { get; set; }
        public int TotalLeave { get; set; }

        public Employee(int id, string name, string designation, double salary, DateTime dateOfBirth, int totalLeave)
        {
            Id = id;
            Name = name;
            Designation = designation;
            Salary = salary;
            DateOfBirth = dateOfBirth;
            TotalLeave = totalLeave;
        }

        public void DoWork()
        {
            Console.WriteLine("Employee does his work");
        }
        public void TakeLeave()
        {
            if (TotalLeave > 0)
            {
                Console.WriteLine("Employee takes leave");
                TotalLeave--;
            }
            else
            {
                Console.WriteLine("Employee has no leave balance");
            }
        }

    }
}
