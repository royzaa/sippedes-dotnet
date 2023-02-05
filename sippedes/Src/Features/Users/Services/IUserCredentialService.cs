using sippedes.Cores.Entities;

namespace sippedes.Features.Users.Services
{
    public interface IUserCredentialService
    {
        Task<UserCredential> GetByEmail(string email);
    }
}
