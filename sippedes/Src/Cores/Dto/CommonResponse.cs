using livecode_net_advanced.Cores.Dto;

namespace sippedes.Cores.Dto;

public class CommonResponse<T> : Response
{
    public int StatusCode { get; set; } = 200;
    public string Message { get; set; } = "Success";
    public T? Data { get; set; }
}