namespace sippedes.Features.Pdf.Services;

public interface IPdfService
{
    Task<IFormFile> GeneratePdf();
}