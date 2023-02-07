using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sippedes.Cores.Controller;
using sippedes.Features.Pdf.Dto;
using sippedes.Features.Pdf.Services;
using sippedes.Src.Cores.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace sippedes.Features.Pdf.Controller;

[Route("api/pdf")]
public class PdfController : BaseController
{
    private readonly IPdfService _pdfService;
    public PdfController(IPdfService pdfService)
    {
        _pdfService = pdfService;
    }
    
    
    [HttpPost("generate")]
    [Authorize(Roles = "Admin")]
    public async Task<dynamic> GeneratePdf(SkckDto legalized)
    {
        var res = await _pdfService.CreateSkckPdf(legalized);

        return Success(res);
    }
    [HttpPost("download")]
    [Authorize(Roles = "Admin")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Download a file.", typeof(FileContentResult))]
    public async Task<FileResult> DownloadPdf(SkckDto legalized)
    {
        var data = await _pdfService.DownloadPdf(legalized);
        
        return File(data, MediaTypeNames.Application.Pdf);
    }
}