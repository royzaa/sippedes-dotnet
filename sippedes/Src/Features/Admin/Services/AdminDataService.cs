using sippedes.Cores.Entities;
using sippedes.Cores.Repositories;

namespace sippedes.Features.Admin.Services
{
    public class AdminDataService : IAdminDataService
    {
        private IRepository<AdminData> _repository;
        private IPersistence _persistence;

        public AdminDataService(IRepository<AdminData> repository, IPersistence persistence)
        {
            _repository = repository;
            _persistence = persistence;
        }

        public async Task<AdminData> CreateAdminData(AdminData request)
        {
            var save = await _repository.Save(request);
            await _persistence.SaveChangesAsync();
            return save;
        }
    }
}
