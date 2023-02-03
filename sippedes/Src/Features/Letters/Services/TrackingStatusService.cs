using sippedes.Commons.Constants;
using sippedes.Cores.Entities;
using sippedes.Cores.Repositories;

namespace sippedes.Features.Letters.Services
{
    public class TrackingStatusService : ITrackingStatusService
    {
        private readonly IRepository<TrackingStatus> _repository;
        private readonly IPersistence _persistence;

        public TrackingStatusService(IRepository<TrackingStatus> repository, IPersistence persistence)
        {
            _repository = repository;
            _persistence = persistence;
        }

        public async Task<TrackingStatus> GetOrSave(EStatus status)
        {
            var statusFind = await _repository.Find(r => r.Status.Equals(status));
            if (statusFind is not null) return statusFind;

            var saveStatus = await _repository.Save(new TrackingStatus { Status = status });
            await _persistence.SaveChangesAsync();
            return saveStatus;
        }
    }
}
