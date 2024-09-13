using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePromotionApp
{
    public class Employee
    {
        int id, age;
        string name;
        double salary;

        public Employee()
        {
        }

        public Employee(int id, int age, string name, double salary)
        {
            this.id = id;
            this.age = age;
            this.name = name;
            this.salary = salary;
        }

        public void TakeEmployeeDetailsFromUser()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please enter the employee ID");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            id = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please enter the employee name");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            name = Console.ReadLine();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please enter the employee age");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            age = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please enter the employee salary");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            salary = Convert.ToDouble(Console.ReadLine());
        }

        public override string ToString()
        {
            return "Employee ID : " + id + "\nName : " + name + "\nAge : " + age + "\nSalary : " + salary;
        }

        public int Id { get => id; set => id = value; }
        public int Age { get => age; set => age = value; }
        public string Name { get => name; set => name = value; }
        public double Salary { get => salary; set => salary = value; }

    }
}
