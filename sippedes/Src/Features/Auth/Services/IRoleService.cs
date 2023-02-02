using sippedes.Commons.Constants;
using sippedes.Cores.Entities;

namespace sippedes.Features.Auth.Services
{
    public interface IRoleService
    {
        Task<Role> GetOrSave(ERole role);
    }
}
