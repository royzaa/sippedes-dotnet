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
        private readonly AppDbContext _appDbContext;
        private readonly IUserCredentialService _userCredentialService;
        private readonly IRepository<Letter> _repository;

        public LetterController(ILetterService letterService, AppDbContext appDbContext, IUserCredentialService userCredentialService, IRepository<Letter> repository)
        {
            _letterService = letterService;
            _appDbContext = appDbContext;
            _userCredentialService = userCredentialService;
            _repository = repository;
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
            var emails = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Email))?.Value;
            var letter = await _letterService.GetBussinessEvidenceLetterById(id, emails);
            return Success(letter);
        }

        [HttpGet("police-record/{id}")]
        public async Task<IActionResult> GetPoliceRecordById(string id)
        {
            var emails = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Email))?.Value;
            var letter = await _letterService.GetPoliceRecordLetterById(id, emails);
            return Success(letter);
        }

        [HttpPut("bussiness-evidence/{id}")]
        public async Task<IActionResult> UpdateBussinessEvidenceLetter([FromBody] BussinessEvidenceLetterRequest request, string id)
        {
            var emails = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Email))?.Value;
            var letterData = await _letterService.UpdateBussinessEvidenceLetter(request, id, emails);
            return Success(letterData);
        }

        [HttpPut("police-record/{id}")]
        public async Task<IActionResult> UpdatePoliceRecordLetter([FromBody] PoliceRecordLetterRequest request, string id)
        {
            var emails = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Email))?.Value;
            var letterData = await _letterService.UpdatePoliceRecordLetter(request, id, emails);
            return Success(letterData);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var emails = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Email))?.Value;
            await _letterService.Delete(id, emails);
            CommonResponse<string?> response = new()
            {
                StatusCode = 200,
                Message = "success",
            };
            return Ok(response);

        }

        
    }
}
