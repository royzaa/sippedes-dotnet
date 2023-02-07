using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace sippedes.Features.Otp.Dto;

public class SendOtpReqDto
{
    // [Required(ErrorMessage = "Email not valid"), EmailAddress]
    internal string Email { get; set; } = null!;
    internal Guid UserId { get; set; } 
    
    internal string? Name { get; set; } 
}

public class VerifyOtpReqDto
{
    [Required]
    public int OtpCode { get; set; }
    
    internal string? UserId { get; set; } 
}