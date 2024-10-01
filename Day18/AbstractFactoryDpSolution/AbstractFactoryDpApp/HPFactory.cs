using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDpApp
{
    internal class HPFactory : IDeviceFactory
    {
        public IDevice CreateMobile()
        {
            return new HPMobile();
        }

        public IDevice CreateLaptop()
        {
            return new HPLaptop();
        }

        public IDevice CreateDesktop()
        {
            return new HPDesktop();
        }
    }
}
