using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Context;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;
using BCrypt.Net;
using System.Text.Json;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly EmployeeRepository _employeeRepository;
    private readonly JwtService _jwtService;

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _employeeRepository = new EmployeeRepository(context);
        _jwtService = new JwtService(configuration, context);
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] JsonElement request)
    {
        if (request.ValueKind == JsonValueKind.Null || request.ValueKind == JsonValueKind.Undefined)
        {
            return BadRequest("Dados inválidos.");
        }

        if (!request.TryGetProperty("email", out var emailElement) || !request.TryGetProperty("password", out var passwordElement))
        {
            return BadRequest("Email e senha são obrigatórios.");
        }

        var email = emailElement.GetString();
        var password = passwordElement.GetString();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return BadRequest("Email e senha são obrigatórios.");
        }

        try
        {
            var employee = _employeeRepository.GetByEmail(email);
            
            if (employee == null)
            {
                return Unauthorized("Credenciais inválidas.");
            }

            // Temporariamente usando comparação simples para teste
            if (password != employee.Password)
            {
                return Unauthorized("Credenciais inválidas.");
            }

            var accessToken = _jwtService.GenerateAccessToken(employee);
            var refreshToken = _jwtService.CreateRefreshToken(employee.Id);

            var response = new
            {
                accessToken = accessToken,
                refreshToken = refreshToken.Token,
                expiresAt = DateTime.UtcNow.AddMinutes(60),
                employee = new
                {
                    id = employee.Id,
                    name = employee.Name,
                    email = employee.Email
                }
            };

            return Ok(response);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno do servidor.");
        }
    }

    [HttpPost("refresh")]
    public ActionResult RefreshToken([FromBody] JsonElement request)
    {
        if (request.ValueKind == JsonValueKind.Null || request.ValueKind == JsonValueKind.Undefined)
        {
            return BadRequest("Dados inválidos.");
        }

        if (!request.TryGetProperty("refreshToken", out var refreshTokenElement))
        {
            return BadRequest("Refresh token é obrigatório.");
        }

        var refreshTokenString = refreshTokenElement.GetString();

        if (string.IsNullOrEmpty(refreshTokenString))
        {
            return BadRequest("Refresh token é obrigatório.");
        }

        try
        {
            var refreshToken = _jwtService.GetRefreshToken(refreshTokenString);
            
            if (refreshToken == null || !_jwtService.ValidateRefreshToken(refreshToken))
            {
                return Unauthorized("Refresh token inválido ou expirado.");
            }

            _jwtService.RevokeRefreshToken(refreshTokenString);

            var accessToken = _jwtService.GenerateAccessToken(refreshToken.Employee);
            var newRefreshToken = _jwtService.CreateRefreshToken(refreshToken.EmployeeId);

            var response = new
            {
                accessToken = accessToken,
                refreshToken = newRefreshToken.Token,
                expiresAt = DateTime.UtcNow.AddMinutes(60),
                employee = new
                {
                    id = refreshToken.Employee.Id,
                    name = refreshToken.Employee.Name,
                    email = refreshToken.Employee.Email
                }
            };

            return Ok(response);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno do servidor.");
        }
    }

    [HttpPost("logout")]
    [Authorize]
    public ActionResult Logout()
    {
        try
        {
            var employeeIdClaim = User.FindFirst("employee_id")?.Value;
            
            if (string.IsNullOrEmpty(employeeIdClaim) || !Guid.TryParse(employeeIdClaim, out var employeeId))
            {
                return BadRequest("Token inválido.");
            }

            _jwtService.RevokeAllRefreshTokens(employeeId);
            
            return Ok(new { message = "Logout realizado com sucesso." });
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno do servidor.");
        }
    }

    [HttpGet("me")]
    [Authorize]
    public ActionResult GetCurrentUser()
    {
        try
        {
            var employeeIdClaim = User.FindFirst("employee_id")?.Value;
            var employeeNameClaim = User.FindFirst("employee_name")?.Value;
            var employeeEmailClaim = User.FindFirst("employee_email")?.Value;

            if (string.IsNullOrEmpty(employeeIdClaim) || !Guid.TryParse(employeeIdClaim, out var employeeId))
            {
                return BadRequest("Token inválido.");
            }

            var employeeInfo = new
            {
                id = employeeId,
                name = employeeNameClaim ?? "",
                email = employeeEmailClaim ?? ""
            };

            return Ok(employeeInfo);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno do servidor.");
        }
    }

    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
