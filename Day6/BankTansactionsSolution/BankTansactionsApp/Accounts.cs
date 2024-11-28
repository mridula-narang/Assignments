using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BankTansactionsApp
{
    internal class Accounts : ITransaction
    {
        private double Balance { get; set; }
        private string AccountType { get; set; } = string.Empty;
        private double TransactionCharge = 0.0;
        private double MinimumBalance = 0.0;
        public Accounts(double balance, string accountType)
        {
            Balance = balance;
            AccountType = accountType;
        }

        public void Deposit(double Amount)
        {
            Balance += Amount;
            Console.WriteLine($"Successfully deposited - Rs. {Amount}. New balance - Rs. {Balance} ");
        }

        public void Withdraw(double Amount)
        {
            if (AccountType == "Normal")
            {
                TransactionCharge = 0.0001 * Amount;
                MinimumBalance = 5000.0;
            }
            else if (AccountType == "NRI")
            {
                TransactionCharge = 0.02 * Amount;
                MinimumBalance = 10000.0;
            }
            else if (AccountType == "Salary")
            {
                TransactionCharge = 0.0;
                MinimumBalance = 0.0;
            }
            double TotalWithdrawalAmount = Amount + TransactionCharge;
            if (Balance - TotalWithdrawalAmount >= MinimumBalance)
            {
                Balance -= TotalWithdrawalAmount;
                Console.WriteLine($"Successfully withdrawn Rs. {Amount}. Transaction charge - Rs. {TransactionCharge}. New balance - Rs. {Balance}");
            }
            else
            {
                Console.WriteLine("Withdrawal not allowed because it would reduce the balance below the minimum required amount.");
            }
        }

        public double GetBalance()
        {
            return Balance;
        }
    }
}
