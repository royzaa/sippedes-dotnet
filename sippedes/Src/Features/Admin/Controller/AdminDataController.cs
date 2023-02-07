using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sippedes.Cores.Controller;
using sippedes.Features.Admin.Dto;
using sippedes.Features.Admin.Services;

namespace sippedes.Features.Admin.Controller;

[Route("api/admin")]
public class AdminController : BaseController
{
    private readonly IAdminDataService _adminDataService;
    
    public AdminController(IAdminDataService adminDataService)
    {
        _adminDataService = adminDataService;
    }
    
    [HttpGet()]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetSelfAdminData()
    {
        var guid = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.PrimarySid))?.Value;
        var res = await _adminDataService.GetAdminDataByUserId(guid);

        return Success(res);
    }
    
    
    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAdmin()
    {
        var res = await _adminDataService.GetAllAdmin();

        return Success(res);
    }
    
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> EditSelfAdminData([FromBody] AdminUpdateReqDto dto)
    {
        var guid = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.PrimarySid))?.Value;
        var res = await _adminDataService.UpdateAdminByUserId(guid, dto);

        return Success(res);
    }
    
    [HttpPut("{user-id}/activate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ActivateAdmin([FromRoute(Name = "user-id")] string id)
    {
        var res = await _adminDataService.ActivateAdminAccount(id);

        return Success(res);
    }
    
    [HttpDelete("{user-id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAdmin([FromRoute(Name = "user-id")] string id)
    { 
        await _adminDataService.DeleteAdminByUserId(id);

        return Ok();
    }
}