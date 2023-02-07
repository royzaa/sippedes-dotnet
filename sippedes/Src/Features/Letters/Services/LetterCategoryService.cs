using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;

namespace sippedes.Features.Letters.Services
{
    
    public class LetterCategoryService : ILetterCategoryService
    {
        private readonly IRepository<LetterCategory> _repository;
        private readonly IPersistence _persistence;

        public LetterCategoryService(IRepository<LetterCategory> repository, IPersistence persistence)
        {
            _repository = repository;
            _persistence = persistence;
        }



        public async Task<LetterCategory> Create(LetterCategory letterCategory)
        {
            var save = await _repository.Save(letterCategory);
            await _persistence.SaveChangesAsync();
            return save;
        }

        public async Task DeleteById(string id)
        {
            var letter = await GetById(id);
            _repository.Delete(letter);
            await _persistence.SaveChangesAsync();
        }

        public async Task<PageResponse<LetterCategory>> GetAllCategories(int page, int size)
        {
            var letterEnumerable = await _repository.FindAll(store => true, page, size);
            var letters = letterEnumerable.ToList();
            return new PageResponse<LetterCategory>
            {
                Content = letters,
                TotalPages = (int)Math.Ceiling((decimal)await _repository.Count() / size),
                TotalElement = letters.Count
            };
        }

        public async Task<LetterCategory> GetById(string id)
        {
            try
            {
                var category = await _repository.FindById(Guid.Parse(id));
                if (category is null) throw new NotFoundException("Category Not Found");
                return category;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<LetterCategory> GetByName(string name)
        {
            try
            {
                var category = await _repository.Find(c => c.Category.Equals(name));
                if (category is null) throw new NotFoundException("Category Not Found");
                return category;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<LetterCategory> Update(LetterCategory letterCategory)
        {
            var update = _repository.Update(letterCategory);
            await _persistence.SaveChangesAsync();
            return update;
        }
    }
}
