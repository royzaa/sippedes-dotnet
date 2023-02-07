using sippedes.Cores.Entities;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;
using sippedes.Features.Admin.Dto;
using sippedes.Features.Users.Services;

namespace sippedes.Features.Admin.Services
{
    public class AdminDataService : IAdminDataService
    {
        private IRepository<AdminData> _repository;
        private IUserCredentialService _userCredentialService;
        private IPersistence _persistence;

        public AdminDataService(IRepository<AdminData> repository, IPersistence persistence, IUserCredentialService userCredentialService)
        {
            _repository = repository;
            _persistence = persistence;
            _userCredentialService = userCredentialService;
        }

        public async Task<AdminData> CreateAdminData(AdminData request)
        {
            var save = await _repository.Save(request);
            await _persistence.SaveChangesAsync();
            return save;
        }

        public async Task<AdminData> UpdateAdminByUserId(string id, AdminUpdateReqDto payload)
        {
            var adminData = await GetAdminDataByUserId(id);

            adminData.FullName = payload.FullName ?? adminData.FullName;

            var updatedData = await _persistence.ExecuteTransactionAsync(async () =>
            {
                var admin = _repository.Update(adminData);

                await _persistence.SaveChangesAsync();

                return admin;
            });
            return adminData;
        }

        public async Task<AdminData> UpdateAdminByUserId(string id, AdminData payload)
        {
            var adminData = await GetAdminDataByUserId(id);

            var updatedData =  await _persistence.ExecuteTransactionAsync(async () =>
            {
                var admin = _repository.Update(adminData);

                await _persistence.SaveChangesAsync();

                return admin;
            });
            
            return updatedData;
        }

        public async Task<AdminData> GetAdminDataByUserId(string id)
        {
            var adminData = await _repository.Find(e => e.UserCredentialId.Equals(Guid.Parse(id)) && e.UserCredential.IsDeleted == 0);

            if (adminData is null) throw new NotFoundException();

            return adminData;
        }

        public async Task<AdminData> ActivateAdminAccount(string id)
        {
            var admin = await GetAdminDataByUserId(id);

            admin.IsActive = 1;

           var updatedData = await UpdateAdminByUserId(id, admin);

           return updatedData;
        }

        public async Task<List<AdminData>> GetAllAdmin()
        {
            return (await _repository.FindAll(e =>  e.UserCredential.IsDeleted == 0)).ToList();
        }

        public async Task DeleteAdminByUserId(string id)
        {
            var admin = await GetAdminDataByUserId(id);

            admin.IsActive = 0;

            await UpdateAdminByUserId(id, admin);

            await _userCredentialService.DeleteAccount(id);
        }
    }
}
