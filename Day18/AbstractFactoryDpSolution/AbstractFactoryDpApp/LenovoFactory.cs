using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDpApp
{
    internal class LenovoFactory : IDeviceFactory
    {
        public IDevice CreateMobile()
        {
            return new LenovoMobile();
        }

        public IDevice CreateLaptop()
        {
            return new LenovoLaptop();
        }

        public IDevice CreateDesktop()
        {
            return new LenovoDesktop();
        }
    }
}
