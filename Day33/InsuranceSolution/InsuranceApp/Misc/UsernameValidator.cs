using System.ComponentModel.DataAnnotations;

namespace InsuranceApp.Misc
{
    public class UsernameValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Username cannot be empty");
            }
            string username = value.ToString() ?? "";
            if (username.Length < 4)
            {
                return new ValidationResult("Username must be at least 4 characters long");
            }
            return ValidationResult.Success;
        }
    }
}
