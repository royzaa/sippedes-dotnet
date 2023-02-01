using sib_api_v3_sdk.Model;
using sippedes.Features.Otp.Dto;

namespace sippedes.Features.Otp.Services;

public interface IOtpService
{
    Task<CreateSmtpEmail> SendOtp(SendOtpReqDto payload);
    Task<VerifyOtpResDto> VerifyOtp(VerifyOtpReqDto payload);
}