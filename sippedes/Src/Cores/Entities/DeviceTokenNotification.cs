using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sippedes.Cores.Entities;

[Table("m_device_token_notification")]
public class DeviceTokenNotification
{
    [Key, Column(name: "id")] public Guid Id { get; set; }
    
    [Column(name:"user_id"), Required] public Guid UserCredentialId { get; set; }

    [Column(name: "device_token")] public string? DeviceToken { get; set; }

    [Column(name: "notification_title")] public string? Title { get; set; }

    [Column(name: "notification_body")] public string? Body { get; set; }

    [Column(name: "device_type")] public string? DeviceType { get; set; }

    [Column(name: "created_at")] public DateTime CreatedAt { get; set; }
    
    public virtual UserCredential? User { get; set; }
}