namespace AppointmentManagementApp.Exceptions
{
    [Serializable]
    internal class AppointmentNotFoundException : Exception
    {
        string msg;
        public AppointmentNotFoundException()
        {
            msg = "Appointment not found";
        }
        public override string Message => msg;
    }
}