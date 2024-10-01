using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDpApp
{
    internal interface IDeviceFactory
    {
        IDevice CreateMobile();
        IDevice CreateLaptop();
        IDevice CreateDesktop();
    }
}
