using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.Repositories;

public class ReaderRepository(AppDbContext context)
{
    public IEnumerable<Reader> GetAll()
    {
        return context.Readers.AsNoTracking();
    }

    public Reader? GetById(Guid readerId)
    {
        return context.Readers
            .Include(x => x.BookLoans)
            .Where(x => x.Id == readerId)
            .AsNoTracking()
            .SingleOrDefault();
    }

    public Reader? GetByEmail(string email)
    {
        return context.Readers
            .Include(x => x.BookLoans)
            .Where(x => x.Email == email)
            .AsNoTracking()
            .SingleOrDefault();
    }

    public void Add(Reader reader)
    {
        context.Readers.Add(reader);
    }

    public void Update(Reader reader)
    {
        context.Readers.Update(reader);
    }
}
