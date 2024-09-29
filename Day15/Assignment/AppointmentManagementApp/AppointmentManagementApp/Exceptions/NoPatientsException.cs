namespace AppointmentManagementApp.Exceptions
{
    [Serializable]
    internal class NoPatientsException : Exception
    {
        string msg;
        public NoPatientsException()
        {
            msg = "No patients found.";
        }
        public override string Message => msg;
    }
}