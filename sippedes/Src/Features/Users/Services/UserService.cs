using Microsoft.EntityFrameworkCore;
using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;
using sippedes.Src.Features.Users.DTO;

namespace sippedes.Src.Features.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IPersistence _persistence;

        public UserService(IRepository<User> repository, IPersistence persistence)
        {
            _repository = repository;
            _persistence = persistence;
        }

        public async Task<UserResponse> CreateNewUser(User payload)
        {
            var user = await _repository.Find(
                user => user.Email.ToLower().Equals(payload.Email.ToLower()));
            if (user is null) 
            {
                var result = await _persistence.ExecuteTransactionAsync(async () =>
                {
                    var save = await _repository.Save(payload);
                    await _persistence.SaveChangesAsync();
                    return save;
                });

                UserResponse response = new()
                {
                    Id = result.Id.ToString(),
                    Email = result.Email,
                    Password = result.Password,
                    Role = result.Role,
                };
                return response;
            }
            
        }

        public Task DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<PageResponse<UserResponse>> GetAll(string? name, int page, int size)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> Update(User payload)
        {
            throw new NotImplementedException();
        }
    }
}
