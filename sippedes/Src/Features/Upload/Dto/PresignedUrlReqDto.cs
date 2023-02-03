using System.ComponentModel.DataAnnotations;

namespace sippedes.Features.Upload.Dto;

public class PresignedUrlReqDto
{
    [Required]
    public string BucketName { get; set; } = null!;
    [Required]
    public string ObjectKey { get; set; } = null!;
    [Required]
    public double Duration { get; set; }
}