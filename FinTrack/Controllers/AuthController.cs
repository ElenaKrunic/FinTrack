using FinTrack.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinTrack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
    {
        if (await _context.Users.AnyAsync(u => u.Username == userRegisterDTO.Username || u.Email == userRegisterDTO.Email))
            return BadRequest("Username or email already exists.");

        var user = new User
        {
            Username = userRegisterDTO.Username,
            Email = userRegisterDTO.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDTO.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User registered successfully.");
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userLoginDTO.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDTO.Password, user.PasswordHash))
            return BadRequest("Invalid username or password.");

        string token = GenerateJwtToken(user);

        return Ok(new { token });
    }
}
