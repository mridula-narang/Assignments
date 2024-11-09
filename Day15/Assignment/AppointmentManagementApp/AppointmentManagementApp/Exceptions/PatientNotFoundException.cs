namespace AppointmentManagementApp.Exceptions
{
    [Serializable]
    internal class PatientNotFoundException : Exception
    {
        string msg;
        public PatientNotFoundException()
        {
            msg = "Patient not found.";
        }
        public override string Message => msg;
    }
}