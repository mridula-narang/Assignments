using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementApp
{
    internal class EmployeeManager
    {
        private Dictionary<int, Employee> employees = new Dictionary<int, Employee>();

        public void AddEmployee()
        {
            try
            {
                Employee emp = new Employee();
                emp.TakeEmployeeDetailsFromUser();
                if (employees.ContainsKey(emp.Id))
                {
                    Console.WriteLine("Employee with this id already exists");
                }
                else
                {
                    employees.Add(emp.Id, emp);
                    Console.WriteLine("Employee added successfully");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter correct data types.");
            }
        }
        public void PrintAllEmployees()
        {
            try
            {
                if (employees.Count == 0)
                {
                    Console.WriteLine("No employees to display.");
                    return;
                }

                foreach (var emp in employees.Values)
                {
                    Console.WriteLine(emp);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while printing employee details: {ex.Message}");
            }
        }
        public void ModifyEmployeeDetails()
        {
            try
            {
                Console.WriteLine("Enter the employee ID to modify:");
                int id = Convert.ToInt32(Console.ReadLine());

                if (employees.TryGetValue(id, out Employee emp))
                {
                    Console.WriteLine("Enter new details for the employee:");
                    Console.WriteLine("Name:");
                    emp.Name = Console.ReadLine();
                    Console.WriteLine("Age:");
                    emp.Age = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Salary:");
                    emp.Salary = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Employee details updated successfully.");
                }
                else
                {
                    Console.WriteLine("Employee with this ID does not exist.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter correct data types.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while modifying employee details: {ex.Message}");
            }
        }

        public void PrintEmployeeById()
        {
            try
            {
                Console.WriteLine("Enter the employee ID to display:");
                int id = Convert.ToInt32(Console.ReadLine());

                if (employees.TryGetValue(id, out Employee emp))
                {
                    Console.WriteLine(emp);
                }
                else
                {
                    Console.WriteLine("Employee with this ID does not exist.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter a valid employee ID.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while printing employee details: {ex.Message}");
            }
        }

        public void DeleteEmployee()
        {
            try
            {
                Console.WriteLine("Enter the employee ID to delete:");
                int id = Convert.ToInt32(Console.ReadLine());

                if (employees.Remove(id))
                {
                    Console.WriteLine("Employee deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Employee with this ID does not exist.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter a valid employee ID.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the employee: {ex.Message}");
            }
        }


    }

}
