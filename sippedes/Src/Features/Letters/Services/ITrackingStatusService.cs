using sippedes.Commons.Constants;
using sippedes.Cores.Entities;

namespace sippedes.Features.Letters.Services
{
    public interface ITrackingStatusService
    {
        Task<TrackingStatus> GetOrSave(EStatus status);
    }
}
