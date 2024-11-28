using AutoMapper;
using InsuraceClaimApp.Interfaces;
using InsuraceClaimApp.Models;

namespace InsuraceClaimApp.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IRepository<int, Policy> _policyRepository;
        private readonly IMapper _mapper;

        public PolicyService(IRepository<int, Policy> policyRepository,IMapper mapper)
        {
            _policyRepository = policyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Policy>> GetAllPoliciesAsync()
        {
            return await _policyRepository.GetAll();
        }

        public async Task<Policy> GetPolicyByIdAsync(int id)
        {
            return await _policyRepository.Get(id);
        }

        public async Task<Policy> AddPolicyAsync(Policy policy)
        {
            return await _policyRepository.Add(policy);
        }
    }
}
