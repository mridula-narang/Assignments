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
        public List<Employee> FindEmployeesByName(string name)
        {
            List<Employee> matchingEmployees = new List<Employee>();

            foreach (var employee in employees.Values)
            {
                if (employee.Name == name)
                {
                    matchingEmployees.Add(employee);
                }
            }

            return matchingEmployees;
        }
    }

}
