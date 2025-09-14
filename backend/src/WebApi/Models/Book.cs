namespace WebApi.Models;

public class Book : TrackableEntity
{
    public string Title { get; private set; } = null!;
    public string Author { get; private set; } = null!;
    public string Isbn { get; private set; } = null!;
    public DateTime PublicationDate { get; private set; }
    public BookStatus Status { get; private set; } = BookStatus.AVAILABLE;
    public BookLoan? BookLoan { get; set; }

    private Book() { }

    public Book(string title, string author, string isbn, DateTime publicationDate, BookStatus? status)
    {
        Title = title;
        Author = author;
        Isbn = isbn;
        PublicationDate = publicationDate;

        if (status is not null) 
        { 
            Status = (BookStatus)status;
        }
    }

    public void Update(Book bookData)
    {
        Title = bookData.Title ?? Title;
        Author = bookData.Author ?? Author;
        Isbn = bookData.Isbn ?? Isbn;
        PublicationDate = bookData.PublicationDate;
        SetStatus(bookData.Status);
        SetUpdateAt(DateTime.UtcNow);
    }

    public void SetStatus(BookStatus status)
    {
        Status = status;
    }
}
