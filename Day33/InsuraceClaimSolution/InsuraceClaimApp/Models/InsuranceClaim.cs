using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuraceClaimApp.Models
{
    public class InsuranceClaim
    {
        [Key]
        public int ClaimId { get; set; }
        public int PolicyNumber { get; set; } 
        public string ClaimType { get; set; } = string.Empty;
        public DateTime IncidentDate { get; set; }
        [ForeignKey("Username")]
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string DocumentPath { get; set; }
    }
}
