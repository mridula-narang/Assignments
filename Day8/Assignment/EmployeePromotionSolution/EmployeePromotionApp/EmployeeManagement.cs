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
        private List<Employee> employeeList = new List<Employee>();
        public void AddEmployee(Employee employee)
        {
            try
            {
                if (employees.ContainsKey(employee.Id))
                {
                    throw new ArgumentException("Duplicate Id.");
                }
                employees.Add(employee.Id, employee);
                employeeList.Add(employee);  // Add to the employee list
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
            List<Employee> foundEmployees = employeeList.Where(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (foundEmployees.Count > 0)
            {
                Console.WriteLine($"Employees with the name '{name}':");
                foreach (Employee emp in foundEmployees)
                {
                    Console.WriteLine(emp);
                }
            }
            else
            {
                Console.WriteLine($"No employees found with the name '{name}'.");
            }
        }
    }

}
