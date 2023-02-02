
using sippedes.Cores.Entities;

namespace sippedes.Features.Admin.Services
{
    public interface IAdminDataService
    {
        Task<AdminData> CreateAdminData(AdminData request);
    }
}
