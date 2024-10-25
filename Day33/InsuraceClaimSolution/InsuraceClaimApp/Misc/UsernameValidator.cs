using System.ComponentModel.DataAnnotations;

namespace InsuraceClaimApp.Misc
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
            if (username.Length < 5)
            {
                return new ValidationResult("Username must be at least 5 characters long");
            }
            if (!char.IsUpper(username[0]))
            {
                return new ValidationResult("Username must begin with a capital letter");
            }
            if (username.IndexOfAny("0123456789!@#$%^&*()".ToCharArray()) != -1)
            {
                return new ValidationResult("Username must not contain digits and special characters");
            }
            return ValidationResult.Success;
        }
    }
}
