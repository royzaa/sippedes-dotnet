using Microsoft.AspNetCore.Mvc;
using sippedes.Cores.Controller;
using sippedes.Features.Upload.Dto;
using sippedes.Features.Upload.Services;

namespace sippedes.Features.Upload.Controllers;

[Route("api/upload")]
public class UploadController: BaseController
{
    private readonly IUploadService _uploadService;

    public UploadController(IUploadService uploadService)
    {
        _uploadService = uploadService;
    }

    [HttpPost("signature")]
    public async Task<IActionResult> UploadSignature(IFormFile formFile)
    {
        var res = await _uploadService.UploadFileSignatureAsync(formFile);

        return Success(res);
    }
    
    [HttpPost("get-presigned-url")]
    public  IActionResult GetPresignurl(PresignedUrlReqDto payload)
    {
        var res =  _uploadService.GeneratePresignedUrl(payload);

        return Success(res);
    }
}