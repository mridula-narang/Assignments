using InsuraceClaimApp.Models.DTOs;

namespace InsuraceClaimApp.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponseDTO> Autheticate(LoginRequestDTO loginUser);
        Task<LoginResponseDTO> Register(UserCreateDTO registerUser);
    }
}
