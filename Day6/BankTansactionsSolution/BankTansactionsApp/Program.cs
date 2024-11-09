using System.Xml.Serialization;

namespace BankTansactionsApp
{
    internal class Program
    {
        ITransaction transaction;
        public Program()
        {
        }
        public void CreateAccount(double balance, string accountType)
        {
            transaction = new Accounts(balance, accountType);
        }
        public void Deposit(double Amount)
        {
            transaction.Deposit(Amount);
        }
        public void Withdraw(double Amount)
        {
            transaction.Withdraw(Amount);
        }
        public double GetBalance()
        {
            return transaction.GetBalance();
        }
        public void DisplayBalance()
        {
            Console.WriteLine($"Current balance - Rs. {GetBalance()}");
        }
        public void DisplayMenu()
        {
            Console.WriteLine("Welcome to ABC bank!! Please Select a service");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Display Balance");
            Console.WriteLine("0. Exit");
        }
        public void CustomerInteraction()
        {
            int choice;
            do
            {
                DisplayMenu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter initial balance");
                        double balance = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Enter account type");
                        string accountType = Console.ReadLine();
                        CreateAccount(balance, accountType);
                        break;
                    case 2:
                        Console.WriteLine("Enter deposit amount");
                        double depositAmount = Convert.ToDouble(Console.ReadLine());
                        Deposit(depositAmount);
                        break;
                    case 3:
                        Console.WriteLine("Enter withdrawal amount");
                        double withdrawalAmount = Convert.ToDouble(Console.ReadLine());
                        Withdraw(withdrawalAmount);
                        break;
                    case 4:
                        DisplayBalance();
                        break;
                    case 0:
                        Console.WriteLine("Thank you for using our services! Have a nice day!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }while (choice!=0);
        }
        static void Main(string[] args)
        {
            var program = new Program();
            program.CustomerInteraction();
        }

    }
}
