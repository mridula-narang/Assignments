namespace PizzaStoreApp.Exceptions
{
    internal class CannotAddWithNoImagesException : Exception
    {
        string msg;
        public CannotAddWithNoImagesException()
        {
            msg = "Cannot add a pizza without images";
        }
        public override string Message => msg;
    }
}