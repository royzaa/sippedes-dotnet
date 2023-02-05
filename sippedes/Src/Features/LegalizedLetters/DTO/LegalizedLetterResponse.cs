namespace sippedes.Src.Features.LegalizedLetter.DTO
{
    public class LegalizedLetterResponse
    {
        public string LegalizedId { get; set; }
        public string LetterId { get; set; }
        public string WitnessSignatureId { get; set; }
        public DateTime Date { get; set; }
        public string PdfUrl { get; set; }  
        public string No { get; set; }
    }
}
