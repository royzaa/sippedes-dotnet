using sippedes.Cores.Entities;

namespace sippedes.Features.Users.Services
{
    public interface IUserCredentialService
    {
        Task<UserCredential> GetByEmail(string email);
        
        Task<UserCredential> GetById(string id);

        Task VerifyAccount(UserCredential user);

        Task DeleteAccount(string id);
    }
}
