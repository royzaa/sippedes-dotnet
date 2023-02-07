namespace sippedes.Cores.Model;

public class PdfApiConf
{
    public string Token { get; set; }
    public string ApiKey { get; set; }
    public string SecretKey { get; set; }
    public string Subject { get; set; }
    public int ExpireInMinutes { get; set; }
    public int TemplateId { get; set; }
}