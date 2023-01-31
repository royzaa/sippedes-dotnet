namespace sippedes.Cores.Dto;

[Obsolete("Experimental Code")]
public  class Response
{
    public int StatusCode { get; set; } 
    public string Message { get; set; }
    public Object? Data { get; set; }
}