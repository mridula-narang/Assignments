namespace VotingApp
{
    internal class Program
    {
        Customer customer;
        IValidateCustomer validateCustomer;
        public Program()
        {
            customer = new PremiumCustomer();
            validateCustomer = new ValidateCustomer();
        }
        void CheckCustomer()
        {
            customer.CustomerDetails();
            validateCustomer.ValidateCustomerByAge(customer);
        }
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please enter a  number");
                int num1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Please enter another  number");
                int num2 = Convert.ToInt32(Console.ReadLine());
                int result = num1 / num2;
                Console.WriteLine($"The result of {num1} / {num2} is {result}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Input was not a number. Please enter a number.");
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("Too big or too small to handle as input");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Cannot divide by zero");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured");
            }
        }
    }
}
