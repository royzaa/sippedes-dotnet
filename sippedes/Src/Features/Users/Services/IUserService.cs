using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Src.Features.Users.DTO;

namespace sippedes.Src.Features.Users.Services
{
    public interface IUserService
    {
        Task<UserResponse> CreateNewUser(User payload);
        Task<PageResponse<UserResponse>> GetAll(string? name, int page, int size);
        Task<UserResponse> GetById(string id);
        Task<UserResponse> Update(User payload);
        Task DeleteById(string id);
    }
}
