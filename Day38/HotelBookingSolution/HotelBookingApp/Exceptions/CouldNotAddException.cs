namespace HotelBookingApp.Exceptions
{
    [Serializable]
    public class CouldNotAddException : Exception
    {
        private string _message;
        public override string Message { get; }

        public CouldNotAddException(string message) : base(message)
        {
            _message = message;
        }

        public CouldNotAddException(string message, Exception innerException) : base(message, innerException)
        {
            _message = message;
        }
    }
}