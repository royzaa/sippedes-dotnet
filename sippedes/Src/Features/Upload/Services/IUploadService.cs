using sippedes.Features.Upload.Dto;

namespace sippedes.Features.Upload.Services;

public interface IUploadService
{
    Task<UploadSignatureRes> UploadFileSignatureAsync(IFormFile file);
    string GeneratePresignedUrl(PresignedUrlReqDto dto);
}