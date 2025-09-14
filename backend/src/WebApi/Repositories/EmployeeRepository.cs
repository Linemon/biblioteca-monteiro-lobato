using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.Repositories;

public class EmployeeRepository(AppDbContext context)
{
    public Employee? GetById(Guid employeeId)
    {
        return context.Employees
            .Include(x => x.RegisteredBookLoans)
            .Where(x => x.Id == employeeId)
            .AsNoTracking()
            .SingleOrDefault();
    }
}
