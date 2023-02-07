using sippedes.Features.Pdf.Dto;
using sippedes.Src.Cores.Entities;

namespace sippedes.Features.Pdf.Services;

public interface IPdfService
{
    Task<ApiResponse> CreateSkckPdf(SkckDto legalized);

    Task<Stream> DownloadPdf(SkckDto dto);
}