namespace AppointmentManagementApp.Exceptions
{
    [Serializable]
    internal class AppointmentOverlapException : Exception
    {
        string msg;
        public AppointmentOverlapException()
        {
            msg = "Appointment overlaps with another appointment. Please book an appointment for another date/time";
        }
        public override string Message => msg;
    }
}