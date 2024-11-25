using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementApp
{
    internal interface ISupplierService
    {
        void UpdateProductQuantity(Supplier supplier, Product product);
        void ViewProductsBySupplier();
        void AddProduct(Supplier supplier, Product product);
    }
}
