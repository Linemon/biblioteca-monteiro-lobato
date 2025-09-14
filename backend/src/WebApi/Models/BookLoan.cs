namespace WebApi.Models;

public class BookLoan : TrackableEntity
{
    public DateTime LoanDate { get; private set; }
    public DateTime PreviousReturnDate { get; private set; }
    public DateTime? RealReturnDate { get; private set; }
    public Guid BookId { get; private set; }
    public Book Book { get; private set; } = null!;
    public Guid ReaderId { get; private set; }
    public Reader Reader { get; private set; } = null!;
    public Guid EmployeeId { get; private set; }
    public Employee Employee { get; private set; } = null!;

    private BookLoan() { }

    internal BookLoan(DateTime loanDate, DateTime previousReturnDate, 
        Guid bookId, Guid readerId, Guid employeeId)
    {
        LoanDate = loanDate;
        PreviousReturnDate = previousReturnDate;
        BookId = bookId;
        ReaderId = readerId;
        EmployeeId = employeeId;
    }

    public BookLoan(Book book, Reader reader, Employee employee)
    {
        LoanDate = DateTime.UtcNow;
        PreviousReturnDate = LoanDate.AddDays(7);
        BookId = book.Id;
        Book = book;
        ReaderId = reader.Id;
        Reader = reader;
        EmployeeId = employee.Id;
        Employee = employee;
    }

    internal BookLoan(Guid bookId, Guid readerId, Guid employeeId)
    {
        BookId = bookId;
        ReaderId = readerId;
        EmployeeId = employeeId;
    }


    public void RegisterBookReturn()
    {
        RealReturnDate = DateTime.UtcNow;
    }

    public bool IsLoanReturnLate()
    {
        return RealReturnDate is null && DateTime.UtcNow > PreviousReturnDate;
    }

    public void NotifyDelayedReturn()
    {
        throw new NotImplementedException();
    }
}
