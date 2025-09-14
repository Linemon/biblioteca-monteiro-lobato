using Microsoft.AspNetCore.Mvc;
using WebApi.Context;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers;

// TODO: autenticação admin
[ApiController]
[Route("api/v1/readers")]
public class ReadersController(AppDbContext context) : ControllerBase
{
    private const string _employeeId = "00000000-0000-0000-0000-000000000000";
    private EmployeeRepository _employeeRepository = new(context);

    [HttpGet]
    public ActionResult<IEnumerable<object>> GetAll()
    {
        var employee = _employeeRepository.GetById(Guid.Parse(_employeeId));

        if (employee is null)
        {
            return Forbid("Employee not found");
        }

        employee.GivePermissions(context);

        var readers = employee.GetReaders();
        return Ok(readers.Select(x => new { x.Id, x.CreatedAt }));
    }

    [HttpGet("{id}")]
    public ActionResult<object> GetById(Guid id)
    {
        var employee = _employeeRepository.GetById(Guid.Parse(_employeeId));

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
                reader.CreatedAt,
                reader.UpdatedAt
            });
        }
        catch (Exception ex) when (ex.Message == "NotFound")
        {
            return NotFound("Reader not found.");
        }
    }

    [HttpPost]
    public ActionResult Create(Reader reader)
    {
        var employee = _employeeRepository.GetById(Guid.Parse(_employeeId));

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

    
    [HttpPut("{id}")]
    public ActionResult Update(Guid id, Reader readerData)
    {
        var employee = _employeeRepository.GetById(Guid.Parse(_employeeId));

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
}
