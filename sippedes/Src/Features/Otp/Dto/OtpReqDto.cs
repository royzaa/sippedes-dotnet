using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace sippedes.Features.Otp.Dto;

public class SendOtpReqDto
{
    [Required(ErrorMessage = "Email not valid"), EmailAddress]
    public string Email { get; set; } = null!;
    public Guid UserId { get; set; } 
}

public class VerifyOtpReqDto
{
    [Required]
    public int OtpCode { get; set; }
    
    public Guid UserId { get; set; } 
}