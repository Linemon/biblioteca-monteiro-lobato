using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.Services;

public class JwtService
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _accessTokenExpirationMinutes;
    private readonly int _refreshTokenExpirationDays;

    public JwtService(IConfiguration configuration, AppDbContext context)
    {
        _configuration = configuration;
        _context = context;
        _secretKey = _configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException("Jwt:SecretKey");
        _issuer = _configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer");
        _audience = _configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience");
        _accessTokenExpirationMinutes = int.Parse(_configuration["Jwt:AccessTokenExpirationMinutes"] ?? "60");
        _refreshTokenExpirationDays = int.Parse(_configuration["Jwt:RefreshTokenExpirationDays"] ?? "7");
    }

    public string GenerateAccessToken(Employee employee)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, employee.Id.ToString()),
            new(ClaimTypes.Name, employee.Name),
            new(ClaimTypes.Email, employee.Email),
            new("employee_id", employee.Id.ToString()),
            new("employee_name", employee.Name),
            new("employee_email", employee.Email)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_accessTokenExpirationMinutes),
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public RefreshToken CreateRefreshToken(Guid employeeId)
    {
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            Token = GenerateRefreshToken(),
            ExpiresAt = DateTime.UtcNow.AddDays(_refreshTokenExpirationDays),
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false
        };

        _context.RefreshTokens.Add(refreshToken);
        _context.SaveChanges();

        return refreshToken;
    }

    public RefreshToken? GetRefreshToken(string token)
    {
        return _context.RefreshTokens
            .Include(rt => rt.Employee)
            .FirstOrDefault(rt => rt.Token == token && !rt.IsRevoked);
    }

    public void RevokeRefreshToken(string token)
    {
        var refreshToken = _context.RefreshTokens
            .FirstOrDefault(rt => rt.Token == token);

        if (refreshToken != null)
        {
            refreshToken.IsRevoked = true;
            _context.SaveChanges();
        }
    }

    public void RevokeAllRefreshTokens(Guid employeeId)
    {
        var refreshTokens = _context.RefreshTokens
            .Where(rt => rt.EmployeeId == employeeId && !rt.IsRevoked)
            .ToList();

        foreach (var token in refreshTokens)
        {
            token.IsRevoked = true;
        }

        _context.SaveChanges();
    }

    public bool ValidateRefreshToken(RefreshToken refreshToken)
    {
        return refreshToken != null && 
               !refreshToken.IsRevoked && 
               refreshToken.ExpiresAt > DateTime.UtcNow;
    }
}
