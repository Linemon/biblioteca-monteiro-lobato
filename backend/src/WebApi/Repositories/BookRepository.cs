using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.Repositories;

public class BookRepository(AppDbContext context)
{
    public Book? GetById(Guid bookId)
    {
        return context.Books
            .Include(x => x.BookLoan)
            .Where(x => x.Id == bookId)
            .AsNoTracking()
            .SingleOrDefault();
    }

    public void Add(Book book)
    {
        context.Books.Add(book);
    }

    public void Update(Book book)
    {
        context.Books.Update(book);
    }

    public void Remove(Guid bookId)
    {
        var book = GetById(bookId);

        if (book is null)
        {
            return;
        }

        context.Books.Remove(book);
    }
}
