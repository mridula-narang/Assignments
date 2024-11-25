using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDesignPatternApp
{
    public class EmailNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine("Sending Email Notification: " + message);
        }
    }
}
