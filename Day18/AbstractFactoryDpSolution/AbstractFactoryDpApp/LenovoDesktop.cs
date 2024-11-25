using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDpApp
{
    internal class LenovoDesktop : IDevice
    {
        public void GetDetails()
        {
            Console.WriteLine("Lenovo Desktop");
        }
    }
}
