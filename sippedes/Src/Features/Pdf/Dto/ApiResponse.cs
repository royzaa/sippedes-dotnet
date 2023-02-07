

using Org.BouncyCastle.Utilities.Encoders;

namespace sippedes.Features.Pdf.Dto;

public class ApiResponse
{
    public Base64  response { get; set; }
    public dynamic meta { get; set; }
}