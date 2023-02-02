using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sippedes.Cores.Entities
{
    [Table(name: "m_admin_data")]
    public class AdminData
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }

        [Column(name: "full_name"), Required]
        public string FullName { get; set; } = null!;

        [Column(name: "is_active")]
        public int IsActive { get; set; }

        [Column(name: "user_credential_id")] public Guid UserCredentialId { get; set; }
        public virtual UserCredential UserCredential { get; set; }
    }
}
