using sippedes.Features.Letters.Dto;
using sippedes.Src.Features.WitnessSignatures.DTO;

namespace sippedes.Features.Pdf.Dto;

public class SkckDto
{
    public string Name { get; set; }
    public string BirthDate { get; set; }
    public string Date { get; set; }
    public string Nationality { get; set; }
    public string MartialStatus { get; set; }
    public string Necessity { get; set; }
    public string Address { get; set; }
    public string Religion { get; set; }
    public string Job { get; set; }
    
    public string WitnessName { get; set; }
    public string Signature { get; set; }
    public string Occupation { get; set; }
    public string No { get; set; }
}