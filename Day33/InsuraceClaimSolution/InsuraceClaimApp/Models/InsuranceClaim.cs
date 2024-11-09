using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuraceClaimApp.Models
{
    public enum ClaimStatus
    {
        Pending,
        Approved,
        Rejected
    }
    public class InsuranceClaim
    {
        [Key]
        public int ClaimId { get; set; }
        public int PolicyNumber { get; set; }
        public string ClaimType { get; set; }
        public DateTime IncidentDate { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
        public string DocumentPath { get; set; }
        public ClaimStatus Status { get; set; } 
    }
   
}
