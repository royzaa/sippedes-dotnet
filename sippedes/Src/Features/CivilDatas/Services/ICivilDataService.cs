using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Features.CivilDatas.DTO;

namespace sippedes.Features.CivilDatas.Services
{
    public interface ICivilDataService
    {
        Task<CivilDataResponse> CreateNewCivil(CivilData payload);
        Task<CivilDataResponse> GetByNIK(string id);
        Task<PageResponse<CivilDataResponse>> GetAllCivil(int page, int size);
        Task<CivilDataResponse> Update(CivilData payload);
        Task DeleteByNIK(string id);
    }
}