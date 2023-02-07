using sippedes.Features.Pdf.Dto;
using sippedes.Src.Cores.Entities;

namespace sippedes.Features.Pdf.Services;

public interface IPdfService
{
    Task<dynamic> CreateSkckPdf(SkckDto legalized);
}