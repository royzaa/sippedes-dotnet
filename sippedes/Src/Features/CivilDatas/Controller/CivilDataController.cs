using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using sippedes.Cores.Controller;
using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Features.CivilDatas.DTO;
using sippedes.Features.CivilDatas.Services;
using sippedes.Features.Users.Services;

namespace sippedes.Src.Features.CivilDatas.Controller
{
    [Route("api/civils")]
    public class CivilDataController : BaseController
    {
        private readonly ICivilDataService _civilService;
        private readonly IUserCredentialService _userCredentialService;

        public CivilDataController(ICivilDataService civilService, IUserCredentialService userCredentialService)
        {
            _civilService = civilService;
            _userCredentialService = userCredentialService;
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
        public async Task<IActionResult> GetAllCivil([FromQuery]int page = 1, [FromQuery] int size = 5)
        {
            var civilData = await _civilService.GetAllCivil(page, size);
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
        
        [HttpGet("profile")]
        public async Task<IActionResult> GetSelfProfile()
        {
            var guid = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.PrimarySid))?.Value;
            var data = await _userCredentialService.GetById(guid);

            CivilDataResponse response = new CivilDataResponse
            {
                NIK = data.CivilData!.NIK,
                NoKK = data.CivilData.NoKK,
                Fullname = data.CivilData.Fullname,
                Gender = data.CivilData.Gender,
                BloodType = data.CivilData.BloodType,
                Education = data.CivilData.Education,
                BirthDate = data.CivilData.BirthDate,
                Address = data.CivilData.Address,
                Province = data.CivilData.Province,
                City = data.CivilData.City,
                District = data.CivilData.District,
                Village = data.CivilData.Village,
                Religion = data.CivilData.Religion
            };
            
            return Success(response);
        }
    }
}
