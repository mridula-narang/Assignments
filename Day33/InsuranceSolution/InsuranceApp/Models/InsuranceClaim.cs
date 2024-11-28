using System.ComponentModel.DataAnnotations;

namespace InsuranceApp.Models
{
    public enum Status
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
        public Status Status { get; set; }

    }
}
