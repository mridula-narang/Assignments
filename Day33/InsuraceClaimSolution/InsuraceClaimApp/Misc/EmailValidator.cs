using System.ComponentModel.DataAnnotations;

namespace InsuraceClaimApp.Misc
{
    public class EmailValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Email cannot be empty");
            }
            if (!((string)value).Contains("@"))
            {
                return new ValidationResult("Please enter a valid email");
            }
            return ValidationResult.Success;
        }
    }
}
