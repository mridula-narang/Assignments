using System.ComponentModel.DataAnnotations;

namespace InsuranceApp.Models
{
    public enum Roles
    {
        Admin,
        Claimant
    }
    public class User
    {
        [Key]
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] HashKey { get; set; }
        public Roles Role { get; set; }
        public ICollection<Policy> Policies { get; set; }
        public ICollection<InsuranceClaim> Claims { get; set; } // Add this line
    }
}
