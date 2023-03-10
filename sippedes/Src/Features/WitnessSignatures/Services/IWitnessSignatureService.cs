using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Src.Features.WitnessSignatures.DTO;

namespace sippedes.Src.Features.WitnessSignatures.Services
{
    public interface IWitnessSignatureService
    {
        Task<WitnessSignatureResponse> CreateNewWitnessSignature(WitnessSignature payload);
        Task<WitnessSignatureResponse> GetWitnessSignatureById(string id);
        Task<PageResponse<WitnessSignatureResponse>> GetAllWitnessSignature(int page, int size);
        Task<WitnessSignatureResponse> UpdateWitnessSignature(WitnessSignature payload);
        Task Delete(string id);
    }
}
