namespace EmployeeExpenseTrackingApp
{
    internal class Program
    {
        Employee CreateEmployee()
        {
            var employee = new Employee();
            Console.WriteLine("Please enter the employee Id");
            employee.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter the employee Name");
            employee.Name = Console.ReadLine() ?? "";
            Console.WriteLine("Please enter the employee Designation");
            employee.Designation = Console.ReadLine() ?? "";
            Console.WriteLine("Please enter the employee Salary");
            employee.Salary = Convert.ToDouble(Console.ReadLine());
            return employee;
        }
        static void Main(string[] args)
        {
            var program = new Program();
            Employee[] employees = new Employee[2];
            for (int i = 0; i < employees.Length; i++)
            {
                employees[i] = program.CreateEmployee();
            }
            for (int i = 0; i < employees.Length; i++)
            {
                Console.WriteLine(employees[i]);
            }
            //create a new expense
            var expense = new Expense();
            expense.AddExpense();
            //display expense details
            expense.ViewExpense();
            //approve the expense
            expense.ChangeStatus("Approved");
            expense.ViewExpense();
        }
    }
}
