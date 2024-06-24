using System.ComponentModel.DataAnnotations;

namespace Cw10.Models;

public class LoginRequestDTO
{
    [Required]
    public string Login { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}