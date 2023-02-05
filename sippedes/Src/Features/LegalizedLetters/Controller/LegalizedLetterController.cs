using Microsoft.AspNetCore.Mvc;
using sippedes.Cores.Controller;
using sippedes.Src.Cores.Entities;
using sippedes.Src.Features.LegalizedLetter.Services;

namespace sippedes.Src.Features.LegalizedLetter.Controller
{
    [Route("api/legalized-letters")]
    public class LegalizedLetterController : BaseController
    {
        private readonly ILegalizedLetterService _legalService;
        public LegalizedLetterController(ILegalizedLetterService legalService)
        {
            _legalService = legalService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLegalizedLetter([FromBody] Legalized request)
        {
            var result = await _legalService.CreateNewLegalizedLetter(request);
            return Created("/api/legalized-letters", Success(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLegalizedLetterById(string id)
        {
            var legalResponse = await _legalService.GetLegalizedLetterById(id);
            return Success(legalResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLegalizedLetter([FromQuery] string? id, [FromQuery] int page = 1, [FromQuery ]int size = 5)
        {
            var result = await _legalService.GetAllLegalizedLetter(id, page, size);
            return Success(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLegalizedLetterById(string id)
        {
            var result = _legalService.Delete(id);
            return Success(result);
        }

    }
}
