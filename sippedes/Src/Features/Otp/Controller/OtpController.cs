using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sib_api_v3_sdk.Model;
using sippedes.Cores.Controller;
using sippedes.Features.Otp.Dto;
using sippedes.Features.Otp.Services;

namespace sippedes.Features.Otp.Controller;

[Route("api/otp")]
public class OtpController : BaseController
{
    private readonly IOtpService _otpService;

    public OtpController(IOtpService otpService)
    {
        _otpService = otpService;
    }

    [HttpPost("send-otp")]
    // [Authorize(Roles = "Civilian")]
    // [AllowAnonymous]
    public async Task<IActionResult> SendOtp([FromBody] SendOtpReqDto payload)
    {
        var guid = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.PrimarySid))?.Value;
        var email = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Email))?.Value;
        payload.UserId = Guid.Parse(guid);
        Console.WriteLine(payload.UserId);
        payload.Email = email;
        var result = await _otpService.SendOtp(payload);

        return Success(result);
    }

    [HttpPost("verify-otp")]
    // [Authorize(Roles = "Civilian")]
    // [AllowAnonymous]
    public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpReqDto payload)
    {
        var guid = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.PrimarySid))?.Value;
        
        payload.UserId = guid;
        
        var result = await _otpService.VerifyOtp(payload);

        return Success(result);
    }
}