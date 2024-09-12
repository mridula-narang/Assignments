using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTansactionsApp
{
    internal interface ITransaction
    {
        public void Deposit(double Amount);
        public void Withdraw(double Amount);
        public double GetBalance();
    }
}
