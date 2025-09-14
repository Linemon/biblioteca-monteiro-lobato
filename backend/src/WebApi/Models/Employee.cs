using WebApi.Context;
using WebApi.Repositories;

namespace WebApi.Models;

public class Employee : User
{
    private BookRepository _bookRepository = null!;
    private BookLoanRepository _bookLoanRepository = null!;
    private ReaderRepository _readerRepository = null!;
    public List<BookLoan> RegisteredBookLoans { get; init; } = [];

    private string _passwordHash = null!;
    public string PasswordHash
    {
        get => _passwordHash;
        set => _passwordHash = value;
    }

    public string Password => PasswordHash;

    private Employee() { }

    public Employee(string name, string email, string phone, string passwordHash)
        : base(name, email, phone) 
    {
        PasswordHash = passwordHash;
    }

    public void GivePermissions(AppDbContext context)
    {
        _bookLoanRepository = new BookLoanRepository(context);
        _bookRepository = new BookRepository(context);
        _readerRepository = new ReaderRepository(context);
    }

    public void RegisterBook(string title, string author, string isbn, DateTime publicationDate, BookStatus status)
    {
        var book = new Book(title, author, isbn, publicationDate, status);
        _bookRepository.Add(book);
    }

    public void UpdateBook(Guid bookId, Book bookData)
    {
        var book = _bookRepository.GetById(bookId);

        if (book is null)
        {
            return;
        }

        book.Update(bookData);
        _bookRepository.Update(book);
    }

    public void RemoveBook(Guid bookId)
    {
        _bookRepository.Remove(bookId);
    }

    public void RegisterBookLoan(Book book, Reader reader)
    {
        var bookLoan = new BookLoan(book, reader, this);
        _bookLoanRepository.Add(bookLoan);
    }

    public void RegisterBookReturn(Guid bookLoanId)
    {
        var bookLoan = _bookLoanRepository.GetById(bookLoanId);

        if (bookLoan is null)
        {
            return;
        }

        bookLoan.RegisterBookReturn();
        _bookLoanRepository.Update(bookLoan);
    }

    public void SendBookDelayReturnNotification(Reader reader)
    {
        throw new NotImplementedException();
    }

    public void RegisterReader(Reader reader)
    {
        var readerWithEmail = _readerRepository.GetByEmail(reader.Email);

        if (readerWithEmail is not null)
        {
            throw new Exception("Conflict");
        }

        _readerRepository.Add(reader);
    }

    public void UpdateReader(Guid readerId, Reader readerData)
    {
        var reader = GetReaderById(readerId);
        var readerWithEmail = _readerRepository.GetByEmail(readerData.Email);

        if (readerWithEmail is not null && readerWithEmail.Id != readerId)
        {
            throw new Exception("Conflict");
        }

        reader.Update(readerData);
        reader.SetStatus(readerData.Status);
        _readerRepository.Update(reader);
    }

    public IEnumerable<Reader> GetReaders()
    {
        return _readerRepository.GetAll();
    }

    public Reader GetReaderById(Guid readerId)
    {
        var reader = _readerRepository.GetById(readerId);

        if (reader is null)
        {
            throw new Exception("NotFound");
        }

        return reader;
    }
}
