

namespace livecode_net_advanced.Cores.Dto;

public class PageResponse<T>
{
    public List<T> Content { get; set; }
    public int TotalPages { get; set; }
    public int TotalElement { get; set; }
}