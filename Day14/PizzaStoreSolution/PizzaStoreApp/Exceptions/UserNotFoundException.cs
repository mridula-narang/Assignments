namespace PizzaStoreApp.Exceptions
{
    [Serializable]
    internal class UserNotFoundException : Exception
    {
        string msg;
        public UserNotFoundException()
        {
            msg = "User not found.";
        }
        public override string Message => base.Message;
    }
}