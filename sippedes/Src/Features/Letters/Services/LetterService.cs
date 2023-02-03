using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;
using sippedes.Features.CivilDatas.Services;
using sippedes.Features.Letters.Dto;

namespace sippedes.Features.Letters.Services
{
    public class LetterService : ILetterService
    {
        private readonly IRepository<Letter> _repository;
        private readonly IPersistence _persistence;
        private readonly ICivilDataService _civilDataService;

        public LetterService(IRepository<Letter> repository, IPersistence persistence, ICivilDataService civilDataService)
        {
            _persistence = persistence;
            _repository = repository;
            _civilDataService = civilDataService;
        }
        public Task<BussinessEvidenceLetterResponse> CreateBussinessEvidenceLetter(BussinessEvidenceLetterRequest request)
        {
            //var verify = await _civilDataService.GetByNIK(request.NIK);
            //if (verify == null) throw new UnauthorizedException("NIK Not Register");

            //var bussinessEvidenceLetterResponse = await _persistence.ExecuteTransactionAsync(async () =>
            //{
            //    var letter = new Letter
            //    {

            //    }
            //})

            throw new NotImplementedException();
        }

        public Task<PoliceRecordLetterResponse> CreatePoliceRecordLetter(PoliceRecordLetterRequest request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBussinessEvidenceLetter(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeletePoliceRecordLetter(string id)
        {
            throw new NotImplementedException();
        }

        public Task<PageResponse<BussinessEvidenceLetterResponse>> GetAllBussinessEvidenceLetter(string? id, int page, int size)
        {
            throw new NotImplementedException();
        }

        public Task<PageResponse<PoliceRecordLetterResponse>> GetAllPoliceRecordLetter(string? id, int page, int size)
        {
            throw new NotImplementedException();
        }

        public Task<BussinessEvidenceLetterResponse> GetBussinessEvidenceLetterById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<PoliceRecordLetterResponse> GetPoliceRecordLetterById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TrackingResponse> GetTrackingBussinessEvidenceLetter(string id)
        {
            throw new NotImplementedException();
        }

        public Task<BussinessEvidenceLetterResponse> UpdateBussinessEvidenceLetter(BussinessEvidenceLetterRequest request)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLetterTracking(string id)
        {
            throw new NotImplementedException();
        }

        public Task<PoliceRecordLetterResponse> UpdatePoliceRecordLetter(PoliceRecordLetterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
