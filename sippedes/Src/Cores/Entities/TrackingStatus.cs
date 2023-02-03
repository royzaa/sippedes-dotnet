using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using sippedes.Commons.Constants;

namespace sippedes.Cores.Entities
{
    [Table(name: "m_tracking_status")]
    public class TrackingStatus
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }

        [Column(name: "status"), Required]
        public EStatus Status { get; set; }

        public virtual ICollection<Letter>? Letters { get; set; }
    }
}