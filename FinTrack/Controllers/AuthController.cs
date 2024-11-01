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
        if (await _context.Users.AnyAsync(u => u.Email == userRegisterDTO.Email))
        {
            return BadRequest("An account with this email already exists. If you've forgotten your password, please use the password recovery option.");
        }

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
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLoginDTO.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDTO.Password, user.PasswordHash))
            return BadRequest("Invalid email or password.");

        string token = GenerateJwtToken(user);

        return Ok(new { token });
    }

    [HttpPost("forgotPassword")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == forgotPasswordDTO.Email);
        if (user == null)
            return BadRequest("User with this email does not exist.");

        string resetToken = GeneratePasswordResetToken(user);

        // ovde cu implementirati slanje mejla sa tokenom korisniku
        // koristiti SendGrid biblioteku 
        return Ok(new { resetToken, message = "Use this token to reset your password." });
    }

    private string GeneratePasswordResetToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(resetPasswordDTO.Token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _context.Users.FindAsync(int.Parse(userId));

            if (user == null)
                return BadRequest("Invalid token.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(resetPasswordDTO.NewPassword);
            await _context.SaveChangesAsync();

            return Ok("Password has been reset successfully.");
        }
        catch (Exception)
        {
            return BadRequest("Invalid or expired token.");
        }
    }
}
