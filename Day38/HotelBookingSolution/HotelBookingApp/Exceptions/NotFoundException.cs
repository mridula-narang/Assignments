using System.Runtime.Serialization;

namespace HotelBookingApp.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception, ISerializable
    {
        string _message;
        public NotFoundException(string entityName)
        {
            _message = $"{entityName} could not be found";
        }
        override public string Message => _message;
        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}