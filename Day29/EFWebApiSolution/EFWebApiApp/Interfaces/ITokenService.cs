using EFWebApiApp.Models.DTO;

namespace EFWebApiApp.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(UserTokenDTO user);
    }
}
