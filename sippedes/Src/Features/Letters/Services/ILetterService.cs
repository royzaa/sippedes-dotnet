using sippedes.Cores.Dto;
using sippedes.Features.Letters.Dto;

namespace sippedes.Features.Letters.Services
{
    public interface ILetterService
    {
        Task<BussinessEvidenceLetterResponse> CreateBussinessEvidenceLetter(BussinessEvidenceLetterRequest request);
        Task<BussinessEvidenceLetterResponse> UpdateBussinessEvidenceLetter(BussinessEvidenceLetterRequest request);
        Task DeleteBussinessEvidenceLetter(string id);
        Task<PageResponse<BussinessEvidenceLetterResponse>> GetAllBussinessEvidenceLetter(string? id, int page, int size);
        Task<BussinessEvidenceLetterResponse> GetBussinessEvidenceLetterById(string id);

        Task<TrackingResponse> GetTrackingBussinessEvidenceLetter(string id);
        Task UpdateLetterTracking(string id);

        Task<PoliceRecordLetterResponse> CreatePoliceRecordLetter(PoliceRecordLetterRequest request);
        Task<PoliceRecordLetterResponse> UpdatePoliceRecordLetter(PoliceRecordLetterRequest request);
        Task DeletePoliceRecordLetter(string id);
        Task<PageResponse<PoliceRecordLetterResponse>> GetAllPoliceRecordLetter(string? id, int page, int size);
        Task<PoliceRecordLetterResponse> GetPoliceRecordLetterById(string id);
    }
}
