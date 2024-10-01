using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDpApp
{
    internal class IBMFactory : IDeviceFactory
    {
        public IDevice CreateMobile()
        {
            return new IBMMobile();
        }

        public IDevice CreateLaptop()
        {
            return new IBMLaptop();
        }

        public IDevice CreateDesktop()
        {
            return new IBMDesktop();
        }
    }
}
