namespace AppointmentManagementApp.Exceptions
{
    [Serializable]
    internal class NoAppointmentsException : Exception
    {
        string msg;
        public NoAppointmentsException()
        {
            msg = "No appointments found";
        }
        public override string Message => msg;
    }
}