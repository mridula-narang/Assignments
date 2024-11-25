using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingOOPSApp
{
    public class Customer: ICustomer
    {
        public void PlaceOrder()
        {
            Console.WriteLine("Order placed successfully!");
        }

        public void CancelOrder()
        {
            Console.WriteLine("Order cancelled successfully!");
        }
    }
}
