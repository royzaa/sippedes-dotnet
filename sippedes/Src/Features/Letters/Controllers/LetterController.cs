using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sippedes.Cores.Controller;
using sippedes.Cores.Database;
using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Cores.Repositories;
using sippedes.Features.Letters.Dto;
using sippedes.Features.Letters.Services;
using sippedes.Features.Users.Services;
using System.Security.Claims;

namespace sippedes.Features.Letters.Controllers
{
    [Route("api/letter")]
    public class LetterController : BaseController
    {
        private readonly ILetterService _letterService;
        private readonly IUserCredentialService _userCredentialService;

        public LetterController(ILetterService letterService, IUserCredentialService userCredentialService)
        {
            _letterService = letterService;
            _userCredentialService = userCredentialService;
        }

        [HttpPost("bussiness-evidence")]
        public async Task<IActionResult> CraeteNewBussinessEvide([FromBody] BussinessEvidenceLetterRequest request)
        {
            var emails = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Email))?.Value;
            var createResponse = await _letterService.CreateBussinessEvidenceLetter(request, emails);
            return Created("/api/letter", Success(createResponse));
        }

        [HttpPost("police-record")]
        public async Task<IActionResult> CreateNewPoliceRecord([FromBody] PoliceRecordLetterRequest request)
        {
            var emails = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Email))?.Value;
            var createResponse = await _letterService.CreatePoliceRecordLetter(request, emails);
            return Created("/api/letter", Success(createResponse));
        }

        [HttpGet("bussiness-evidence")]
        public async Task<IActionResult> GetAllBussinessEvidenceLetter([FromQuery] int page = 1, [FromQuery] int size = 5)
        {
            var letterData = await _letterService.GetAllBussinessEvidenceLetter(page, size);
            return Success(letterData);
        }

        [HttpGet("police-record")]
        public async Task<IActionResult> GetAllPoliceRecord([FromQuery] int page = 1, [FromQuery] int size = 5)
        {
            var letterData = await _letterService.GetAllPoliceRecordLetter(page, size);
            return Success(letterData);
        }

        [HttpGet("bussiness-evidence/{id}")]
        public async Task<IActionResult> GetBussinessEvidenceById(string id)
        {
            var letter = await _letterService.GetBussinessEvidenceLetterById(id);
            return Success(letter);
        }

        [HttpGet("police-record/{id}")]
        public async Task<IActionResult> GetPoliceRecordById(string id)
        {
            var letter = await _letterService.GetPoliceRecordLetterById(id);
            return Success(letter);
        }

        [HttpPut("bussiness-evidence/{id}")]
        public async Task<IActionResult> UpdateBussinessEvidenceLetter([FromBody] BussinessEvidenceLetterRequest request, string id)
        {
            var letterData = await _letterService.UpdateBussinessEvidenceLetter(request, id);
            return Success(letterData);
        }

        [HttpPut("police-record/{id}")]
        public async Task<IActionResult> UpdatePoliceRecordLetter([FromBody] PoliceRecordLetterRequest request, string id)
        {
            var letterData = await _letterService.UpdatePoliceRecordLetter(request, id);
            return Success(letterData);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _letterService.Delete(id);
            CommonResponse<string?> response = new()
            {
                StatusCode = 200,
                Message = "success",
            };
            return Ok(response);

        }

        
    }
}
