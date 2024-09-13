namespace EmployeePromotionApp
{
    internal class Program
    {
        //EmployeePromotion employeePromotion = new EmployeePromotion();
        
        static void Main(string[] args)
        {

            try
            {
                var program = new Program();
                //program.employeePromotion.InputEmployeeDetails();
                //program.employeePromotion.DisplayPromotionOrder(); //method to be called for easy question 1
                //program.employeePromotion.FindEmployeePosition(); //method to be called for easy question 2
                //program.employeePromotion.RemoveSpace(); //method to be called for easy question 3
                //program.employeePromotion.SortEmployeeNames(); //method to be called for easy question 4

                EmployeeManagement employeeManagement = new EmployeeManagement();
                Employee employee1 = new Employee();
                employee1.TakeEmployeeDetailsFromUser();
                employeeManagement.AddEmployee(employee1);

                Employee employee2 = new Employee();
                employee1.TakeEmployeeDetailsFromUser();
                employeeManagement.AddEmployee(employee1);

                //Console.WriteLine("Enter the employee Id to retrieve details: ");
                //int id = Convert.ToInt32(Console.ReadLine());
                //employeeManagement.GetEmployeeById(id);

                //var employeesList = employeeManagement.GetAllEmployees();
                //employeesList.Sort(new SalaryComparer());

                //Console.WriteLine("Employees sorted by salary:");
                //foreach (var emp in employeesList)
                //{
                //    Console.WriteLine(emp);
                //}

                Console.WriteLine("Enter the employee name to be searched ");
                string searchName = Console.ReadLine();
                employeeManagement.FindEmployeesByName(searchName);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("One or both employees are null");

            }
            
            
        }
    }
}
