using sippedes.Cores.Dto;
using sippedes.Cores.Entities;

namespace sippedes.Features.Letters.Services
{
    public interface ILetterCategoryService
    {
        Task<LetterCategory> Create(LetterCategory letterCategory);
        Task<LetterCategory> GetById(string id);
        Task<LetterCategory> GetByName(string name);
        Task<LetterCategory> Update(LetterCategory letterCategory);
        Task DeleteById(string id);
        Task<PageResponse<LetterCategory>> GetAllCategories(int page, int size);
    }
}
