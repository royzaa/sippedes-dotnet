
using Microsoft.AspNetCore.Mvc;
using sippedes.Cores.Controller;
using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Src.Features.CivilDatas.Services;

namespace sippedes.Src.Features.CivilDatas.Controller
{
    [Route("api/civils")]
    public class CivilDataController : BaseController
    {
        private readonly ICivilDataService _civilService;

        public CivilDataController(ICivilDataService civilService)
        {
            _civilService = civilService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewCivil([FromBody] CivilData request)
        {
            var createResponse = await _civilService.CreateNewCivil(request);
            return Created("/api/civils", Success(createResponse));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCivilByNIK(string id)
        {
            var civilResponse = await _civilService.GetByNIK(id);
            return Success(civilResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCivil([FromQuery] string? id, [FromQuery]int page = 1, [FromQuery] int size = 5)
        {
            var civilData = await _civilService.GetAllCivil(id, page, size);
            return Success(civilData);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCivilByNIK(string id)
        {
            await _civilService.DeleteByNIK(id);
            CommonResponse<string?> response = new()
            {
                StatusCode = 200,
                Message = "success",
            };
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CivilData civilData)
        {
            var updatedData = await _civilService.Update(civilData);
            return Success(updatedData);
        }
    }
}
