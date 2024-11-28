using InsuranceApp.Models;

namespace InsuranceApp.Interfaces
{
    public interface IPolicyService
    {
        Task<Policy> AddPolicyAsync(Policy policy);
        Task<IEnumerable<Policy>> GetAllPoliciesAsync();
        Task<Policy> GetPolicyByIdAsync(int id);
    }
}
