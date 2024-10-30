using System.ComponentModel.DataAnnotations;

namespace InsuraceClaimApp.Models
{
    public enum Roles
    {
        Admin,
        Claimant
    }
    public class User
    {
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] HashKey { get; set; }
        public Roles Role { get; set; }
        public ICollection<Policy> Policies { get; set; } 
    }
}
