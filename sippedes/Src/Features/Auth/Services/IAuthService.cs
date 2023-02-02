using sippedes.Features.Auth.Dto;

namespace sippedes.Features.Auth.Services
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterAdmin(RegisterRequest request);
        Task<RegisterResponse> RegisterCivilin(RegisterCivilinRequest request);
        Task<LoginResponse> Login(LoginRequest request);
    }
}
