namespace IssueTrackingApp
{
    internal class Program
    {
        //create employee object by getting data from user in console and return back the object

        Employee CreateEmployee()
        {
            var employee = new Employee();
            Console.WriteLine("Enter Employee Id");
            employee.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Employee Name");
            employee.Name = Console.ReadLine()??"";
            Console.WriteLine("Enter Employee Designation");
            employee.Designation = Console.ReadLine()??"";
            Console.WriteLine("Enter Employee Salary");
            employee.Salary = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Employee Date of Birth");
            employee.DateOfBirth = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter Employee Total Leave");
            employee.TotalLeave = Convert.ToInt32(Console.ReadLine());
            return employee;
        }
        static void Main(string[] args)
        {
            //var program = new Program();
            Employee employee1 = new Employee(101, "John", "Manager", 50000, new DateTime(1980, 1, 1), 10);
            //Employee employee2 = new Program().CreateEmployee();
            Employee employee2 = new Employee(102, "Jane", "Developer", 40000, new DateTime(1985, 1, 1), 15);
            Issue issue1 = new Issue(1, "Chair Unavailable", "The working chair is not available in the floor", 102);
            issue1.AssignIssue(101);
            issue1.ChangeStatus("Closed");
            //issue1.PrintIssueDetails();
            Console.WriteLine(issue1);
        }
    }
}
