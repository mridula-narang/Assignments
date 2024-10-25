using InsuraceClaimApp.Exceptions;
using InsuraceClaimApp.Interfaces;
using InsuraceClaimApp.Models.DTOs;
using InsuraceClaimApp.Models;
using AutoMapper;

namespace InsuraceClaimApp.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IRepository<int, InsuranceClaim> _claimRepository;
        private readonly IRepository<int, Policy> _policyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ClaimService> _logger;

        public ClaimService(IRepository<int, InsuranceClaim> claimRepository, IRepository<int, Policy> policyRepository,IMapper mapper, ILogger<ClaimService> logger)
        {
            _claimRepository = claimRepository;
            _policyRepository = policyRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<InsuranceClaim> AddClaimAsync(ClaimRequestDTO claimRequest)
        {
            var policy = await _policyRepository.Get(claimRequest.PolicyId);
            if (policy == null)
            {
                throw new NotFoundException("Policy");
            }

            var newClaim = new InsuranceClaim
            {
                PolicyNumber = claimRequest.PolicyId,
                ClaimType = claimRequest.ClaimType,
                IncidentDate = claimRequest.IncidentDate,
                UserName = claimRequest.UserName,
                PhoneNumber = claimRequest.PhoneNumber,
                Email = claimRequest.Email,
                DocumentPath = await SaveDocumentAsync(claimRequest.Document) 
            };

            return await _claimRepository.Add(newClaim);
        }

        public async Task<InsuranceClaim> GetClaimByIdAsync(int id)
        {
            return await _claimRepository.Get(id);
        }

        public async Task<IEnumerable<InsuranceClaim>> GetAllClaimsAsync()
        {
            return await _claimRepository.GetAll();
        }

        public async Task<InsuranceClaim> UpdateClaimAsync(int id, ClaimRequestDTO claimRequest)
        {
            var existingClaim = await _claimRepository.Get(id);
            if (existingClaim == null)
            {
                throw new NotFoundException("Claim");
            }

            existingClaim.ClaimType = claimRequest.ClaimType;
            existingClaim.IncidentDate = claimRequest.IncidentDate;
            existingClaim.UserName = claimRequest.UserName;
            existingClaim.PhoneNumber = claimRequest.PhoneNumber;
            existingClaim.Email = claimRequest.Email;

            if (claimRequest.Document != null)
            {
                existingClaim.DocumentPath = await SaveDocumentAsync(claimRequest.Document);
            }

            return await _claimRepository.Update(id, existingClaim);
        }

        public async Task DeleteClaimAsync(int id)
        {
            await _claimRepository.Delete(id);
        }

        public async Task<string> SaveDocumentAsync(IFormFile document)
        {
            var folderPath = Path.Combine("wwwroot", "documents");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (document == null)
            {
                return null; // or some default path, if needed
            }

            var filePath = Path.Combine(folderPath, document.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await document.CopyToAsync(stream);
            }

            return filePath; 
        }
    }
}
