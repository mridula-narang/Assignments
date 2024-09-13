using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePromotionApp
{
    internal class EmployeeManagement
    {
        private Dictionary<int, Employee> employees = new Dictionary<int, Employee>();
       // private List<Employee> employeeList = new List<Employee>();
        public void AddEmployee(Employee employee)
        {
            try
            {
                if (employees.ContainsKey(employee.Id))
                {
                    throw new ArgumentException("Duplicate Id.");
                }
                employees.Add(employee.Id, employee);
                //employeeList.Add(employee);  // Add to the employee list
                Console.WriteLine("Employee added successfully.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Duplicate Id. Employee ID must be unique");
            }
        }
        public void GetEmployeeById(int id)
        {
            try
            {
                if (!employees.ContainsKey(id))
                {
                    throw new KeyNotFoundException("Employee Id not found.");
                }
                Employee employee = employees[id];
                Console.WriteLine(employee);
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine("Employee with the provided ID does not exist.");
            }
        }
        public List<Employee> GetAllEmployees()
        {
            return employees.Values.ToList();
        }


        public void FindEmployeesByName(string name)
        {
            //List<Employee> matchingEmployees = new List<Employee>();

            foreach (var employee in employees.Values)
            {

                if (employee.Name == name)
                {

                    Console.WriteLine($"Employee id: {employee.Id}");
                    Console.WriteLine($"Employee name: {employee.Name}");
                    Console.WriteLine($"Emloyee age:{employee.Age}");
                    Console.WriteLine($"Employee salary:{employee.Salary}");
                    Console.WriteLine("----------------------------------");
                }
            }

        }
        public void FindElderEmployees()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEnter the ID of the employee to find elders:");
            Console.ResetColor();

            int id = Convert.ToInt32(Console.ReadLine());

            if (employees.ContainsKey(id))
            {
                Employee givenEmployee = employees[id];
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Employees older than {givenEmployee.Name}:");
                Console.ResetColor();

                bool foundElder = false; // Flag to track if any older employees are found

                foreach (var employee in employees.Values)
                {
                    if (employee.Age > givenEmployee.Age)
                    {
                        Console.WriteLine(employee.Name);
                        foundElder = true;
                    }
                }

                if (!foundElder)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No employees older than the specified employee.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee ID not found.");
                Console.ResetColor();
            }
        }

    }

}
