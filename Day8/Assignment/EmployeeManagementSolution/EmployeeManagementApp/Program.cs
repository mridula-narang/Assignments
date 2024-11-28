namespace EmployeeManagementApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EmployeeManager manager = new EmployeeManager();
            bool running = true;

            while (running)
            {
                try
                {
                    Console.WriteLine("Menu:");
                    Console.WriteLine("1. Print all employees");
                    Console.WriteLine("2. Add employee");
                    Console.WriteLine("3. Modify employee details");
                    Console.WriteLine("4. Print employee by ID");
                    Console.WriteLine("5. Delete employee");
                    Console.WriteLine("6. Exit");
                    Console.Write("Choose an option: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            manager.PrintAllEmployees();
                            break;
                        case "2":
                            manager.AddEmployee();
                            break;
                        case "3":
                            manager.ModifyEmployeeDetails();
                            break;
                        case "4":
                            manager.PrintEmployeeById();
                            break;
                        case "5":
                            manager.DeleteEmployee();
                            break;
                        case "6":
                            running = false;
                            Console.WriteLine("Exiting...");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }
    }
}
