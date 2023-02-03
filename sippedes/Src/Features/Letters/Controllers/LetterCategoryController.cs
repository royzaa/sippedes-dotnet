using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sippedes.Cores.Controller;
using sippedes.Cores.Entities;
using sippedes.Features.Letters.Services;

namespace sippedes.Features.Letters.Controllers
{
    [Route("api/letters/categories")]
    public class LetterCategoryController : BaseController
    {
        private readonly ILetterCategoryService _letterCategoryService;

        public LetterCategoryController(ILetterCategoryService letterCategoryService)
        {
            _letterCategoryService = letterCategoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLettterCategoryById(string id)
        {
            var letter = await _letterCategoryService.GetById(id);
            return Success(letter);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewCategoryLetter([FromBody] LetterCategory request)
        {
            var createResponse = await _letterCategoryService.Create(request);
            return Created("/api/letters/categories", Success(createResponse));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategories([FromQuery] int page = 1, [FromQuery] int size = 5)
        {
            var categories = await _letterCategoryService.GetAllCategories(page, size);
            return Success(categories);
        }
    }
}
