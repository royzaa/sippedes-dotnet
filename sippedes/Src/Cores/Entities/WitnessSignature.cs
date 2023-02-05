using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sippedes.Src.Cores.Entities
{
    [Table(name: "m_witness_signature")]
    public class WitnessSignature
    {
        [Key, Column(name:"id")] public Guid Id { get; set; }
        [Column(name:"witness_name")] public string WitnessName { get; set; }
        [Column(name:"signature")] public string Signature { get; set; }
        [Column(name: "occupation")] public string Occupation { get; set; }
        [Column(name: "is_active")] public int IsActive { get; set; }
    }
}
