using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementApp
{
    internal interface ICustomerService
    {
        void BuyProduct(Customer customer, Product product);
        void ViewAllProducts();
    }
}
