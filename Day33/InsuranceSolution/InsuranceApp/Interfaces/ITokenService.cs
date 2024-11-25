using InsuranceApp.Models.DTOs;

namespace InsuranceApp.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(UserTokenDTO user);
    }
}
