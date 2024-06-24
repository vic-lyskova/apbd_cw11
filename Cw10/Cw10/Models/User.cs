using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cw10.Models;

[Table("Users")]
public class User
{
    [Key]
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Salt { get; set; }
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExp { get; set; }
}