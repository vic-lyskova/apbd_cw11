namespace Cw10.Models;

public class LoginResponseDTO
{
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}