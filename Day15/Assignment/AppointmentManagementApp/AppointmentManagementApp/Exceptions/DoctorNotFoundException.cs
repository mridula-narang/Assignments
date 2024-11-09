namespace AppointmentManagementApp.Exceptions
{
    [Serializable]
    internal class DoctorNotFoundException : Exception
    {
        string msg;
        public DoctorNotFoundException()
        {
            msg = "Doctor not found.";
        }
        public override string Message => msg;
    }
}