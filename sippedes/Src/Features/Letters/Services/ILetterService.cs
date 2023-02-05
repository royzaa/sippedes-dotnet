using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Features.Letters.Dto;
using static Amazon.S3.Util.S3EventNotification;
using System.Linq.Expressions;

namespace sippedes.Features.Letters.Services
{
    public interface ILetterService
    {
        Task<BussinessEvidenceLetterResponse> CreateBussinessEvidenceLetter(BussinessEvidenceLetterRequest request, string email);
        Task<BussinessEvidenceLetterResponse> UpdateBussinessEvidenceLetter(BussinessEvidenceLetterRequest request, string id, string email);
        Task<PageResponse<BussinessEvidenceLetterResponse>> GetAllBussinessEvidenceLetter(int page, int size);
        Task<BussinessEvidenceLetterResponse> GetBussinessEvidenceLetterById(string id, string email);

        Task UpdateLetterTrackingToOnProcess(string id);
        Task UpdateLetterTrackingToComplete(string id);
        Task Delete(string id, string email);


        Task<PoliceRecordLetterResponse> CreatePoliceRecordLetter(PoliceRecordLetterRequest request, string email);
        Task<PoliceRecordLetterResponse> UpdatePoliceRecordLetter(PoliceRecordLetterRequest request, string id, string email);
        Task<PageResponse<PoliceRecordLetterResponse>> GetAllPoliceRecordLetter(int page, int size);
        Task<PoliceRecordLetterResponse> GetPoliceRecordLetterById(string id, string email);
    }
}
