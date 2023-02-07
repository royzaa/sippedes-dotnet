using System.Net.Mime;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using sippedes.Cores.Model;
using sippedes.Cores.Security;
using sippedes.Features.Pdf.Dto;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace sippedes.Features.Pdf.Services;

public class PdfService : IPdfService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<PdfApiConf> _pdfSecret;
    private readonly IJwtUtils _jwtUtils;

    public PdfService(IHttpClientFactory httpClientFactory, IOptions<PdfApiConf> pdfSecret, IJwtUtils jwtUtils)
    {
        _httpClientFactory = httpClientFactory;
        _pdfSecret = pdfSecret;
        _jwtUtils = jwtUtils;
    }

    public async Task<dynamic> CreateSkckPdf(SkckDto legalized)
    {
        var templateId = _pdfSecret.Value.TemplateId;
        var jsonEncode =
            $"{{\"occupation\":\"{legalized.Occupation}\",\"signature\":\"{legalized.Signature}\",\"witnessName\":\"{legalized.WitnessName}\",\"no\":\"{legalized.No}\",\"religion\":\"{legalized.Religion}\",\"martialStatus\":\"{legalized.MartialStatus}\",\"name\":\"{legalized.Name}\",\"birthdate\":\"{legalized.BirthDate}\",\"nationality\":\"{legalized.Nationality}\",\"address\":\"{legalized.Address}\",\"job\":\"{legalized.Job}\",\"necessity\":\"{legalized.Necessity}\"}}";
        var body = new StringContent(
            jsonEncode,
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
        var token = _jwtUtils.GeneratePdfToken(_pdfSecret.Value);

        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Post,
            $"https://us1.pdfgeneratorapi.com/api/v3/templates/{templateId}/output?output=url")
        {
            Headers =
            {
                {
                    HeaderNames.Authorization,
                    $"Bearer {token}"
                },
            },
            Content = body
        };

        var httpClient = _httpClientFactory.CreateClient();


        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            Console.WriteLine("failed");
            Console.WriteLine(httpResponseMessage.StatusCode);
        }

        return JsonObject.Parse(await httpResponseMessage.Content.ReadAsStringAsync());
    }
}