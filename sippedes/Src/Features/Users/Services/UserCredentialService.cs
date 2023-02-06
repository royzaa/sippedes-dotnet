using sippedes.Cores.Entities;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;

namespace sippedes.Features.Users.Services
{
    public class UserCredentialService : IUserCredentialService
    {
        private readonly IRepository<UserCredential> _repository;
        private readonly IPersistence _persistence;

        public UserCredentialService(IRepository<UserCredential> repository, IPersistence persistence)
        {
            _repository = repository;
            _persistence = persistence;
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

        public async Task<UserCredential> GetById(string id)
        {
            try
            {
                var users = await _repository.Find(users => users.Id.Equals(Guid.Parse(id)));
                if (users is null) throw new NotFoundException();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task DeleteAccount(string id)
        {
            var user = await GetById(id);

            user.IsDeleted = 1;
            user.Email = BCrypt.Net.BCrypt.HashString(user.Email);

           await _persistence.ExecuteTransactionAsync(async () =>
            {
                var res =  _repository.Update(user);
                await _persistence.SaveChangesAsync();
                return res;
            });
        }
    }
}