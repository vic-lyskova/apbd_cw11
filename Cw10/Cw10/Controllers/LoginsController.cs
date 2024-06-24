using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Cw10.Context;
using Cw10.Helpers;
using Cw10.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Cw10.Controllers;

public class LoginsController : ControllerBase
{
    private readonly ApbdContext _context;
    private readonly IConfiguration _configuration;

    public LoginsController(IConfiguration configuration, ApbdContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult RegisterStudent(RegisterRequestDTO model)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);

        var user = new User()
        {
            Login = model.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequestDTO model)
    {
        User user;
        try
        {
            user = _context.Users.First(u => u.Login.Equals(model.Login));
        }
        catch (InvalidOperationException)
        {
            return NotFound("No user with this login found");
        }

        var password = user.Password;
        var providedPassword = SecurityHelpers.GetHashedPasswordWithSalt(model.Password, user.Salt);

        if (!password.Equals(providedPassword))
        {
            return Unauthorized("Wrong username or password");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescription = new SecurityTokenDescriptor()
        {
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"],
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!)),
                SecurityAlgorithms.HmacSha256
            )
        };
        var token = tokenHandler.CreateToken(tokenDescription);
        var stringToken = tokenHandler.WriteToken(token);

        var refTokenDescription = new SecurityTokenDescriptor
        {
            Issuer = _configuration["JWT:RefIssuer"],
            Audience = _configuration["JWT:RefAudience"],
            Expires = DateTime.Now.AddDays(3),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:RefKey"]!)),
                SecurityAlgorithms.HmacSha256
            )
        };
        var refToken = tokenHandler.CreateToken(refTokenDescription);
        var stringRefToken = tokenHandler.WriteToken(refToken);

        user.RefreshToken = stringRefToken;
        user.RefreshTokenExp = DateTime.Now.AddDays(3);
        
        _context.SaveChanges();
        
        return Ok(new LoginResponseDTO
        {
            Token = stringToken,
            RefreshToken = stringRefToken
        });
    }
    
    [HttpPost("refresh")]
    public IActionResult RefreshToken(RefreshTokenRequestDTO requestDto)
    {
        User user;
        try
        {
            user = _context.Users.First(u => u.RefreshToken.Equals(requestDto.RefreshToken));
        }
        catch (InvalidOperationException)
        {
            return NotFound("Invalid refresh token");
        }
        
        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescription = new SecurityTokenDescriptor()
        {
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"],
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!)),
                SecurityAlgorithms.HmacSha256
            )
        };
        var token = tokenHandler.CreateToken(tokenDescription);
        var stringToken = tokenHandler.WriteToken(token);
        
        var refTokenDescription = new SecurityTokenDescriptor
        {
            Issuer = _configuration["JWT:RefIssuer"],
            Audience = _configuration["JWT:RefAudience"],
            Expires = DateTime.UtcNow.AddDays(3),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:RefKey"]!)),
                SecurityAlgorithms.HmacSha256
            )
        };
        var refToken = tokenHandler.CreateToken(refTokenDescription);
        var stringRefToken = tokenHandler.WriteToken(refToken);
        
        user.RefreshToken = stringRefToken;
        user.RefreshTokenExp = DateTime.Now.AddDays(3);

        _context.SaveChanges();
        
        return Ok(new LoginResponseDTO()
        {
            Token = stringToken,
            RefreshToken = stringRefToken
        });
    }
}