namespace PizzaStoreApp.Exceptions
{
    public class NoSuchPizzaException : Exception
    {
        string msg;
        public NoSuchPizzaException()
        {
            msg = "Pizza with the given details not found in the repository";
        }
        public override string Message => msg;
    }
}
