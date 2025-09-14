using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Context;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/readers")]
[Authorize]
public class ReadersController(AppDbContext context) : ControllerBase
{
    private EmployeeRepository _employeeRepository = new(context);

    private Guid GetCurrentEmployeeId()
    {
        var employeeIdClaim = User.FindFirst("employee_id")?.Value;
        if (string.IsNullOrEmpty(employeeIdClaim) || !Guid.TryParse(employeeIdClaim, out var employeeId))
        {
            throw new UnauthorizedAccessException("Token inválido ou funcionário não encontrado.");
        }
        return employeeId;
    }

    [HttpGet]
    public ActionResult<IEnumerable<object>> GetAll()
    {
        try
        {
            var employeeId = GetCurrentEmployeeId();
            var employee = _employeeRepository.GetById(employeeId);

            if (employee is null)
            {
                return Forbid("Employee not found");
            }

            employee.GivePermissions(context);

            var readers = employee.GetReaders();
            return Ok(readers.Select(x => new { x.Id, x.CreatedAt }));
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Token inválido ou funcionário não encontrado.");
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno do servidor.");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<object> GetById(Guid id)
    {
        try
        {
            var employeeId = GetCurrentEmployeeId();
            var employee = _employeeRepository.GetById(employeeId);

            if (employee is null)
            {
                return Forbid("Employee not found");
            }

            employee.GivePermissions(context);

            try
            {
                var reader = employee.GetReaderById(id);

                return Ok(new
                {
                    reader.Name,
                    reader.Email,
                    reader.Phone,
                    status = (int)reader.Status,
                    reader.CreatedAt,
                    reader.UpdatedAt
                });
            }
            catch (Exception ex) when (ex.Message == "NotFound")
            {
                return NotFound("Reader not found.");
            }
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Token inválido ou funcionário não encontrado.");
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno do servidor.");
        }
    }

    [HttpPost]
    public ActionResult Create(Reader reader)
    {
        try
        {
            var employeeId = GetCurrentEmployeeId();
            var employee = _employeeRepository.GetById(employeeId);

            if (employee is null)
            {
                return Forbid("Employee not found");
            }

            employee.GivePermissions(context);

            if (reader is null)
            {
                return BadRequest();
            }

            try
            {
                employee.RegisterReader(reader);
                context.SaveChanges();

                return CreatedAtAction(nameof(GetById), new { id = reader.Id }, new { reader.Id });
            } 
            catch (Exception ex) when (ex.Message == "Conflict")
            {
                return Conflict("There is already a reader using this email.");
            }
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Token inválido ou funcionário não encontrado.");
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno do servidor.");
        }
    }

    
    [HttpPut("{id}")]
    public ActionResult Update(Guid id, Reader readerData)
    {
        try
        {
            var employeeId = GetCurrentEmployeeId();
            var employee = _employeeRepository.GetById(employeeId);

            if (employee is null)
            {
                return Forbid("Employee not found");
            }

            employee.GivePermissions(context);

            if (readerData is null)
            {
                return BadRequest();
            }

            try
            {
                employee.UpdateReader(id, readerData);
                context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex) when (ex.Message == "NotFound")
            {
                return NotFound("Reader not found.");
            }
            catch (Exception ex) when (ex.Message == "Conflict")
            {
                return Conflict("There is already a reader using this email.");
            }
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Token inválido ou funcionário não encontrado.");
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno do servidor.");
        }
    }
}
