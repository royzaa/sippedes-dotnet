using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sippedes.Cores.Entities;

[Table(name: "m_user_credential")]
public class UserCredential
{
    [Key, Column(name: "id")] public Guid Id { get; set; }

    [Column(name: "email"), Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Column(name: "password"), Required, StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    public string Password { get; set; } = null!;

    [Column(name: "role_id")]
    public Guid RoleId { get; set; }

    [Column(name: "is_verifed")]
    public int IsVerifed { get; set; }

    [Column(name: "civil_data_id")]
    public string? CivilDataId { get; set; }
    
    [Column(name: "is_deleted")]
    public int IsDeleted { get; set; }

    public virtual CivilData? CivilData { get; set; }
    public virtual Role Role { get; set; }
}