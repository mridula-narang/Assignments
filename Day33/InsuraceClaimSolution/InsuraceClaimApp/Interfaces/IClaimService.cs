using InsuraceClaimApp.Models;
using InsuraceClaimApp.Models.DTOs;

namespace InsuraceClaimApp.Interfaces
{
    public interface IClaimService
    {
        Task<InsuranceClaim> AddClaimAsync(ClaimRequestDTO claimRequest);
        Task<InsuranceClaim> GetClaimByIdAsync(int id);
        Task<IEnumerable<InsuranceClaim>> GetAllClaimsAsync();
        Task<InsuranceClaim> UpdateClaimAsync(int id, ClaimRequestDTO claimRequest);
        Task DeleteClaimAsync(int id);
    }
}
