using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sippedes.Commons.Constants;

namespace sippedes.Cores.Entities;

public class User
{
    [Key, Column(name: "id")] public Guid Id { get; set; }

    [Column(name: "email"), Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Column(name: "password"), Required, StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    public string Password { get; set; } = null!;

    [Column(name: "role")]
    public ERole Role { get; set; }
}