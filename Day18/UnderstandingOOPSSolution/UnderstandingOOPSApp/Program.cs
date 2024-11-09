namespace UnderstandingOOPSApp
{
    internal class Program
    {
        //create instances of the DigitalProduct and PhysicalProduct classes within a method
        void CreateProductInstances()
        {
            var digitalProduct = new DigitalProduct("Learning C#: E-Book", 10.0, "PDF");
            digitalProduct.DisplayProductInfo();
            Console.WriteLine($"Delivery Method: {digitalProduct.DeliveryMethod()}");
            Console.WriteLine($"File Format: {digitalProduct.FileFormat}");

            var physicalProduct = new PhysicalProduct("Laptop", 1000.0, "1234, 5th Avenue, New York");
            physicalProduct.DisplayProductInfo();
            Console.WriteLine($"Delivery Method: {physicalProduct.DeliveryMethod()}");
            Console.WriteLine($"Shipping Address: {physicalProduct.ShippingAddress}");
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.CreateProductInstances();
            ICustomer customer = new Customer();
            customer.PlaceOrder();   
            customer.CancelOrder();
        }
    }
}
