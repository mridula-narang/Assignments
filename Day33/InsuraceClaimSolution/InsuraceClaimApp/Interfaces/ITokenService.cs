using InsuraceClaimApp.Models.DTOs;

namespace InsuraceClaimApp.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(UserTokenDTO user);
    }
}
