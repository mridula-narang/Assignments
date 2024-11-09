namespace PizzaStoreApp.Exceptions
{
    internal class DuplicateUsernameException : Exception
    {
        string msg;
        public DuplicateUsernameException()
        {
            msg = "This username already exists. Please enter a different username.";
        }
        public override string Message => msg;
    }
}