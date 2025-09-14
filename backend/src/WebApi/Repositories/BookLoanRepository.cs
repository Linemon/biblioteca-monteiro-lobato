using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.Repositories;

public class BookLoanRepository(AppDbContext context)
{
    public BookLoan? GetById(Guid bookLoanId)
    {
        return context.BookLoans
            .Include(x => x.Book)
            .Where(x => x.Id == bookLoanId)
            .AsNoTracking()
            .SingleOrDefault();
    }

    public void Add(BookLoan bookLoan)
    {
        context.BookLoans.Add(bookLoan);
    }

    public void Update(BookLoan bookLoan)
    {
        context.BookLoans.Update(bookLoan);
    }
}
