using livecode_net_advanced.Cores.Dto;

namespace sippedes.Cores.Dto;

public class ErrorResponse : Response
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}