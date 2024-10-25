using System.ComponentModel.DataAnnotations;

namespace InsuraceClaimApp.Models
{
    public class User
    {
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] HashKey { get; set; }
        public ICollection<Policy> Policies { get; set; } 
    }
}
