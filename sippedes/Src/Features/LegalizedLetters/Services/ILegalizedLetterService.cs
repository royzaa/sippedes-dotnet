using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Src.Cores.Entities;
using sippedes.Src.Features.LegalizedLetter.DTO;

namespace sippedes.Src.Features.LegalizedLetter.Services
{
    public interface ILegalizedLetterService
    {
        Task<LegalizedLetterResponse> CreateNewLegalizedLetter(Legalized payload);
        Task<LegalizedLetterResponse> GetLegalizedLetterById(string id);
        Task<PageResponse<LegalizedLetterResponse>> GetAllLegalizedLetter(string? id, int page, int size);
        Task<LegalizedLetterResponse> Update(Letter payload);
        Task Delete(string id);
    }
}
