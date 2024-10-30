using InsuraceClaimApp.Misc;
using System.ComponentModel.DataAnnotations;

namespace InsuraceClaimApp.Models.DTOs
{
    public class ClaimRequestDTO
    {
        public int PolicyId { get; set; }
        public string ClaimType { get; set; }
        public DateTime IncidentDate { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public IFormFile Document { get; set; }
        public decimal Amount { get; set; } // Added Amount property
    }
}
