using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sippedes.Cores.Entities
{
    [Table(name: "m_letter_category")]
    public class LetterCategory
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }

        [Column(name: "category"), Required]
        public string Category { get; set; } = null!;

        //public virtual ICollection<Letter>? Letters { get; set; }
    }
}