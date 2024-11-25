namespace ProductManagementApp
{
    internal class Program
    {
        ICustomerService customerService;
        ISupplierService supplierService;
        public Program()
        {
            customerService = new ProductServices();
            supplierService = new ProductServices();
        }
        public void PrintMenu()
        {
            Console.WriteLine("Welcome to shopping application.");
            Console.WriteLine("Enter 1 if you are a customer");
            Console.WriteLine("Enter 2 if you are a supplier");
            Console.WriteLine("Enter 0 to exit");
        }

        public void CustomerMenu()
        {
            Console.WriteLine("Welcome dear customer! We are happy to have you back to our store");
            Console.WriteLine("Enter 1 to buy a product");
            Console.WriteLine("Enter 2 to view all products");
            Console.WriteLine("Enter 0 to go back to main menu");
        }

        public void SupplierMenu() 
        {
            Console.WriteLine("Welcome dear supplier!");
            Console.WriteLine("Enter 1 to add new product");
            Console.WriteLine("Enter 2 to view all products");
            Console.WriteLine("Enter 3 to update product quantity");
            Console.WriteLine("Enter 0 to go back to main menu");
        }
        void MainInteractionConsole()
        {
            int choice;
            do
            {
                PrintMenu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        CustomerInteractionConsole();
                        break;
                    case 2:
                        SupplierInteractionConsole();
                        break;
                    case 0:
                        Console.WriteLine("Thank you for shopping with us. Have a nice day!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid choice");
                        break;
                }
            }
            while (choice != 0);
        }
        void CustomerInteractionConsole()
        {
            int choice;
            do
            {
                CustomerMenu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        BuyProduct();
                        break;
                    case 2:
                        customerService.ViewAllProducts();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid choice");
                        break;
                }
            }
            while (true);
        }
        void SupplierInteractionConsole()
        {
            int choice;
            do
            {
                SupplierMenu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddNewProduct();
                        break;
                    case 2:
                        supplierService.ViewProductsBySupplier();
                        break;
                    case 3:
                        UpdateProductQuantity();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid choice");
                        break;
                }
            }
            while (true);
        }
        private void BuyProduct()
        {
            Console.WriteLine("Enter your customer id:");
            int customerId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Product ID to Buy:");
            int productId = Convert.ToInt32(Console.ReadLine());

            Customer customer = new Customer { Id = customerId }; 
            Product product = new Product { Id = productId }; 
            customerService.BuyProduct(customer, product);
        }
        private void AddNewProduct()
        {
            Console.WriteLine("Enter Supplier ID:");
            int supplierId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Product ID:");
            int productId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Product Name:");
            string productName = Console.ReadLine();
            Console.WriteLine("Enter Product Price:");
            int price = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Quantity:");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Supplier supplier = new Supplier { Id = supplierId }; 
            Product product = new Product { Id = productId, Name = productName, Price = price, Quantity = quantity };
            supplierService.AddProduct(supplier, product);
        }
        private void UpdateProductQuantity()
        {
            Console.WriteLine("Enter Supplier ID:");
            int supplierId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Product ID to Update Quantity:");
            int productId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter New Quantity:");
            int newQuantity = Convert.ToInt32(Console.ReadLine());

            Supplier supplier = new Supplier { Id = supplierId }; // Assuming supplier is created
            Product product = new Product { Id = productId, Quantity = newQuantity };
            supplierService.UpdateProductQuantity(supplier, product); // Assuming this updates quantity
        }
        static void Main(string[] args)
        {
            var program = new Program();
            program.MainInteractionConsole();
        }
    }
}
