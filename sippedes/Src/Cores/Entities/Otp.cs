using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sippedes.Cores.Entities;

[Table(name:"m_otp")]
public class Otp
{
    [Key, Column(name: "id")] public Guid id { get; set; } 
    
    [Column(name:"user_id"), Required] public Guid user_id { get; set; }
    
    [Column(name:"otp_code")] public int OtpCode { get; set; }
    
    [Column(name:"is_expired")] public short IsExpired { get; set; }
    
    [Column(name:"last_expiration")] public DateTime LastExpiration { get; set; }
    
    [Column(name:"created_at")] public DateTime CreatedAt { get; set; }

    public virtual User? User { get; set; }
}