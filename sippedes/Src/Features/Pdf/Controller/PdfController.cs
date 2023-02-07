using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sippedes.Cores.Controller;
using sippedes.Features.Pdf.Dto;
using sippedes.Features.Pdf.Services;
using sippedes.Src.Cores.Entities;

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

        return res;
    }
}