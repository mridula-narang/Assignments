using System.Runtime.CompilerServices;

namespace UnderstandingDelegateApp
{
    internal class Program
    {
        class Employee
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Salary { get; set; }
            public override string ToString()
            {
                return "Id = " + ID + ", Name = " + Name + ", Salary = " + Salary;
            }
        }
        List<Employee> employees = new List<Employee>()
        {
            new Employee { ID = 101, Name = "Mark", Salary = 5000 },
            new Employee { ID = 102, Name = "John", Salary = 14000 },
            new Employee { ID = 103, Name = "Smith", Salary = 5500 },
            new Employee { ID = 104, Name = "Watson", Salary = 15000 }
        };
        public Program()
        {
            UnderstandingLINQ();
        }

        private void UnderstandingLINQ()
        {
            //var highPaidEmployees = employees.Where(emp => emp.Salary > 10000 && emp.Name.Contains("oh"));
            //var highPaidEmployees = employees.Where(e => e.Name.Contains("o")).OrderBy(emp => emp.Salary);
            //var highPaidEmployees = employees.Where(e => e.Name.Contains("o")).OrderByDescending(emp => emp.Salary);
            var highPaidEmployees = employees.Where(e => e.Name.Contains("o")).OrderByDescending(emp => emp.Salary).Take(1); // Take(1) will return only one record. if we use Take(2) then it will return 2 records.
            foreach (var employee in highPaidEmployees)
            {
                Console.WriteLine(employee);
            }
        }

        static void Main(string[] args)
        {
            new Program();
        }
    }
}