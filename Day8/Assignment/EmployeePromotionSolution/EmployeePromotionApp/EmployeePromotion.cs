using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePromotionApp
{
    internal class EmployeePromotion
    {
        
        

        List<Employee> employees;
        public EmployeePromotion()
        {
            employees = new List<Employee>();
        }

        //takes employee names in the order in which they are eligible for promotion.
        public void InputEmployeeDetails()
        {
            string name;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please enter the employee names in the order of their eligibility for promotion. (enter blank to stop): ");
            Console.ResetColor();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                name = Console.ReadLine();
                Console.ResetColor();

                if (string.IsNullOrWhiteSpace(name))
                {
                    break;
                }
                Employee employee = new Employee();
                employee.Name = name;
                employees.Add(employee);
            }            

        }

        //Solution for question 1 - easy level
        public void DisplayPromotionOrder()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n Pomoted employee list: \n");
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Name);
            }
            Console.ResetColor();
        }

        //Solution for question 2 - easy level
        public void FindEmployeePosition()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please enter the name of the employee to check promotion position");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            string name = Console.ReadLine();
            Console.ResetColor();
            int position = employees.FindIndex(e => e.Name == name);
            Console.ForegroundColor = ConsoleColor.Green;
            if (position == -1)
            {
                Console.WriteLine("Employee not found in the promotion list");
            }
            else
            {
                Console.WriteLine($"{name} is the position {position + 1} for promotion");
            }
            Console.ResetColor();
        }
        //Solution for question 4 - easy level
        public void SortEmployeeNames()
        {
            var sortedEmployees = employees.OrderBy(e => e.Name).ToList();
            //print the sorted list
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Promoted Employee list: ");
            foreach (var employee in sortedEmployees)
            {
                Console.WriteLine(employee.Name);
            }
            Console.ResetColor();
        }
        public void RemoveSpace()
        {
            //solution for question 3 - easy level
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"The current size of the collection is {employees.Capacity}");
            employees.TrimExcess();
            Console.WriteLine($"The size after removing extra space is {employees.Count}");
            Console.ResetColor();
        }
        
    }
}
