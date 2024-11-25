namespace AppointmentManagementApp.Exceptions
{
    public class DuplicatePatientException : Exception
    {
        string msg;
        public DuplicatePatientException()
        {
            msg = "Patient with this username already exists. Please try again with a different username.";
        }
        public override string Message => msg;
    }
}
