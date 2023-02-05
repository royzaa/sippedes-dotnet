using sippedes.Cores.Entities;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;

namespace sippedes.Features.Users.Services
{
    public class UserCredentialService : IUserCredentialService
    {
        private readonly IRepository<UserCredential> _repository;

        public UserCredentialService(IRepository<UserCredential> repository)
        {
            _repository = repository;
        }

        public async Task<UserCredential> GetByEmail(string email)
        {
            try
            {
                var users = await _repository.Find(users => users.Email.Equals(email));
                if (users is null) throw new NotFoundException("Email Not Found");
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
