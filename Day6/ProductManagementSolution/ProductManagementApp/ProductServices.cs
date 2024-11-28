using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementApp
{
    internal class ProductServices : ICustomerService, ISupplierService
    {
        Product[] products = new Product[10];
        Customer[] customers = new Customer[4];
        Supplier[] suppliers = new Supplier[4];
        private int count;
        public ProductServices()
        {
            products = new Product[]
            {
                new Product { Id = 1, Name = "Laptop", Price = 500000, Quantity = 5, SupplierId = 1 },
                new Product { Id = 2, Name = "Mobile", Price = 20000, Quantity = 10, SupplierId = 3 },
                new Product { Id = 3, Name = "Tablet", Price = 10000, Quantity = 15, SupplierId = 4 },
                new Product { Id = 4, Name = "Desktop", Price = 30000, Quantity = 20, SupplierId = 2 }
            };
            count = products.Length;
            // Initialize arrays with actual customers
            customers = new Customer[]
            {
                new Customer { Id = 1, Name = "James", Phone = "6749545709" },
                new Customer { Id = 2, Name = "Harry", Phone = "7684757888" },
                new Customer { Id = 3, Name = "Ron", Phone = "8475663846" },
                new Customer { Id = 4, Name = "Hermione", Phone = "8993766658" }
            };

            // Initialize arrays with actual suppliers
            suppliers = new Supplier[]
            {
                new Supplier { Id = 1, Name = "Dell" },
                new Supplier { Id = 2, Name = "Apple" },
                new Supplier { Id = 3, Name = "Samsung" },
                new Supplier { Id = 4, Name = "HP" }
            };


        }

        private Product GetProductById(int pid)
        {
            Product product = null;
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].Id == pid)
                {
                    product = products[i];
                    break;
                }
            }
            return product;
        }

        public void BuyProduct(Customer customer, Product product)
        {
            if (customer == null)
            {
                Console.WriteLine("Customer not found");
                return;
            }

            Product ProductToBuy = GetProductById(product.Id);
            if (ProductToBuy != null)
            {
                if (ProductToBuy.Quantity > 0)
                {
                    ProductToBuy.Quantity--;
                    Console.WriteLine("Thank you for buying the product.");
                }
                else
                {
                    Console.WriteLine("Product is out of stock.");
                }
            }
            else
            {
                Console.WriteLine("Product not found");
            }
        }


        public void ViewAllProducts()
        {
            foreach (var product in products)
            {
                Console.WriteLine($"Product Id: {product.Id}, Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
            }
        }

        public void UpdateProductQuantity(Supplier supplier, Product product)
        {
            //allow supplier to increase quantity of products in the list
            Product ProductToAdd = GetProductById(product.Id);
            if (ProductToAdd != null)
            {
                ProductToAdd.Quantity++;
                Console.WriteLine("Product quantity updated");
            }
            else
            {
                Console.WriteLine("Product not found");
            }
        }

        public void ViewProductsBySupplier()
        {
            Console.WriteLine("Enter supplier ID to view products:");
            int supplierId = Convert.ToInt32(Console.ReadLine());

            bool found = false;
            foreach (var product in products)
            {
                if (product != null && product.SupplierId == supplierId) 
                {
                    Console.WriteLine($"Product Id: {product.Id}, Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("No products found for the given supplier ID.");
            }
        }

        //specific supplier can add new product
        public void AddProduct(Supplier supplier, Product product)
        {
            bool supplierAuthorized = Array.Exists(suppliers, s => s.Id == supplier.Id);

            if (supplierAuthorized)
            {
                if (GetProductById(product.Id) != null)
                {
                    Console.WriteLine("Product already exists.");
                }
                else
                {
                    // Check if there's enough space in the array, if not, resize
                    if (count >= products.Length)
                    {
                        Array.Resize(ref products, products.Length * 2);
                    }

                    product.SupplierId = supplier.Id;
                    products[count] = product;
                    count++;
                    Console.WriteLine("Product added successfully.");
                }
            }
            else
            {
                Console.WriteLine("Supplier not authorized to add products.");
            }
        }



    }
}
