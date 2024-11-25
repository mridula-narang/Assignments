namespace AppointmentManagementApp.Exceptions
{
    internal class DuplicateDoctorException : Exception
    {
        string msg;
        public DuplicateDoctorException()
        {
            msg = "Doctor already exists.";
        }
        public override string Message => msg;
    }
}