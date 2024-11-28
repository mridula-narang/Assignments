using InsuranceApp.Models.DTOs;
using InsuranceApp.Models;

namespace InsuranceApp.Interfaces
{
    public interface IClaimService
    {
        Task<InsuranceClaim> AddClaimAsync(ClaimRequestDTO claimRequest);
        Task<InsuranceClaim> GetClaimByIdAsync(int id);
        Task<IEnumerable<InsuranceClaim>> GetAllClaimsAsync();
        Task<InsuranceClaim> UpdateClaimAsync(int id, ClaimRequestDTO claimRequest);
        Task DeleteClaimAsync(int id);
        Task<InsuranceClaim> ApproveClaimAsync(int id);
    }
}
