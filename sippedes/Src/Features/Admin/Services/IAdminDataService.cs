
using sippedes.Cores.Entities;
using sippedes.Features.Admin.Dto;

namespace sippedes.Features.Admin.Services
{
    public interface IAdminDataService
    {
        Task<AdminData> CreateAdminData(AdminData request);
        Task<AdminData> UpdateAdminByUserId(string id, AdminUpdateReqDto payload);
        
        Task<AdminData> UpdateAdminByUserId(string id, AdminData payload);
        
        Task<AdminData> GetAdminDataByUserId(string id);
        
        Task<AdminData> ActivateAdminAccount(string id);

        Task<List<AdminData>> GetAllAdmin();

        Task DeleteAdminByUserId(string id);
    }
}
