using Microsoft.EntityFrameworkCore;
using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;
using sippedes.Src.Cores.Entities;
using sippedes.Src.Features.LegalizedLetter.DTO;

namespace sippedes.Src.Features.LegalizedLetter.Services
{
    public class LegalizedLetterService : ILegalizedLetterService
    {
        private readonly IRepository<Legalized> _legalRepository;
        private readonly IRepository<Letter> _letterRepository;
        private readonly IRepository<WitnessSignature> _witnessRepository;
        private readonly IPersistence _persistence;

        public LegalizedLetterService(IRepository<Legalized> legalRepository, IRepository<Letter> letterRepository, IRepository<WitnessSignature> witnessRepository, IPersistence persistence)
        {
            _legalRepository = legalRepository;
            _persistence = persistence;
            _letterRepository = letterRepository;
        }

        public async Task<LegalizedLetterResponse> CreateNewLegalizedLetter(Legalized payload)
        {
            var validateLetter = await _letterRepository.Find(letter => letter.Id.ToString().Equals(payload.LetterId));
            if (validateLetter is null) throw new NotFoundException("Letter Id Not Found");

            var validateWitness = await _witnessRepository.Find(witness => witness.Id.ToString().Equals(payload.WitnessSignatureId));
            if (validateWitness is null) throw new NotFoundException("Witness Signature Id Not Found");

            try
            {
                var result = await _persistence.ExecuteTransactionAsync(async () =>
                {
                    var save = await _legalRepository.Save(payload);
                    await _persistence.SaveChangesAsync();
                    return save;
                });
                LegalizedLetterResponse response = new()
                {
                    LegalizedId = result.LegalizedId.ToString(),
                    LetterId = result.LetterId.ToString(),
                    WitnessSignatureId = result.WitnessSignatureId.ToString(),
                    Date = result.Date,
                    PdfUrl = result.PdfUrl,
                    No = result.No
                };
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task Delete(string id)
        {
            var result = await _legalRepository.Find(letter => letter.LegalizedId.Equals(id));
            if (result is null) throw new NotFoundException("Legalized Letter Not Found");
            _legalRepository.Delete(result);
            await _persistence.SaveChangesAsync();
        }

        public async Task<PageResponse<LegalizedLetterResponse>> GetAllLegalizedLetter(int page, int size)
        {
            var result = await _legalRepository.FindAll(
                criteria: letter => true,
                page: page,
                size: size
                );

            var legalizedLetterResponse = result.Select(data => new LegalizedLetterResponse
            {
                LegalizedId = data.LegalizedId.ToString(),
                LetterId = data.LetterId.ToString(),
                WitnessSignatureId = data.WitnessSignatureId.ToString(),
                Date = data.Date,
                PdfUrl = data.PdfUrl,
                No = data.No
            }).ToList();

            var totalPages = (int)Math.Ceiling(await _legalRepository.Count() / (decimal)size);
            PageResponse<LegalizedLetterResponse> pageResponse = new()
            {
                Content = legalizedLetterResponse,
                TotalPages = totalPages,
                TotalElement = legalizedLetterResponse.Count()
            };
            return pageResponse;
        }

        public async Task<LegalizedLetterResponse> GetLegalizedLetterById(string id)
        {
            try
            {
                var result = await _legalRepository.Find(letter => letter.LegalizedId.Equals(id));
                if (result is null) throw new NotFoundException("Legalized Letter NotFound");

                LegalizedLetterResponse response = new()
                {
                    LegalizedId = result.LegalizedId.ToString(),
                    LetterId = result.LetterId.ToString(),
                    WitnessSignatureId = result.WitnessSignatureId.ToString(),
                    Date = result.Date,
                    PdfUrl = result.PdfUrl,
                    No = result.No
                };
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public Task<LegalizedLetterResponse> Update(Letter payload)
        {
            throw new NotImplementedException();
        }
    }
}
