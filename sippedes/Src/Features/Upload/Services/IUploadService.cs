using sippedes.Features.Upload.Dto;

namespace sippedes.Features.Upload.Services;

public interface IUploadService
{
    Task<string> UploadFileSignatureAsync(IFormFile file);
    string GeneratePresignedUrl(PresignedUrlReqDto dto);
}