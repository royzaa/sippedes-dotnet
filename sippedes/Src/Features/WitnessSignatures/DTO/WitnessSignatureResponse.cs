namespace sippedes.Src.Features.WitnessSignatures.DTO
{
    public class WitnessSignatureResponse
    {
        public Guid Id { get; set; }
        public string WitnessName { get; set; }
        public string Signature { get; set; }
        public string Occupation { get; set; }
        public int IsActive { get; set; }
    }
}
