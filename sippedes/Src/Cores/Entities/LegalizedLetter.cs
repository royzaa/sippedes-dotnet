using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sippedes.Cores.Entities 
{ 
    [Table("m_legalized_letter")] 
    public class Legalized 
    { 
        [Key, Column(name: "legalized_id")] public Guid LegalizedId { get; set; } 
        [Column(name: "letter_id")] public Guid LetterId { get; set; } 
        [Column(name: "whitness_signature_id")] public Guid WitnessSignatureId { get; set; } 
        [Column(name: "date")] public DateTime Date { get; set; } 
        [Column(name: "pdf_url")] public string PdfUrl { get; set; } 
        [Column(name: "no")] public string No { get; set; } 
 
        public virtual Letter? Letter { get; set; } 
        public virtual WitnessSignature? WitnessSignature { get; set; } 
    } 
}