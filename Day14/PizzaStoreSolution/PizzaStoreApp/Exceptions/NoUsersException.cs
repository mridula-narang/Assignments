namespace PizzaStoreApp.Exceptions
{
    [Serializable]
    internal class NoUsersException : Exception
    {
        string msg;
        public NoUsersException()
        {
            msg = "No users found";
        }
        public override string Message => msg;
    }
}