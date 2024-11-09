using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDpApp
{
    internal class AppleFactory : IDeviceFactory
    {
        public IDevice CreateMobile()
        {
            return new AppleMobile();
        }

        public IDevice CreateLaptop()
        {
            return new AppleLaptop();
        }

        public IDevice CreateDesktop()
        {
            return new AppleDesktop();
        }
    }
}
