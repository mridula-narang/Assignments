using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp
{
    internal class PremiumCustomer : Customer
    {
        public string Membership { get; set; }
        public PremiumCustomer()
        {
            Membership = "Gold";
        }
        public override void CustomerDetails()
        {
            base.CustomerDetails();
            Console.WriteLine("Enter the membership type:");
            Membership = Console.ReadLine() ?? "Gold";
        }
    }
}
