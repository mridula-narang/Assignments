namespace PizzaStoreApp.Exceptions
{
    [Serializable]
    internal class NoPizzasException : Exception
    {
        string msg;
        public NoPizzasException()
        {
            msg = "There are no more pizzas. You ate them all!!";
        }
        public override string Message => msg;

    }
}