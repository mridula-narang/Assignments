namespace AppointmentManagementApp.Exceptions
{
    [Serializable]
    internal class NoDoctorsException : Exception
    {
        string msg;
        public NoDoctorsException()
        {
            msg = "No doctors found.";
        }
        public override string Message => msg;
    }
}