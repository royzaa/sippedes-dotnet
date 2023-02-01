namespace sippedes.Features.Otp.Dto;

public class SendOtpResDto
{
    public int OtpCode { get; set; }
}

public class VerifyOtpResDto
{
    public bool Success { get; set; }
}