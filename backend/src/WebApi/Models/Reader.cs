using System.Text.Json.Serialization;

namespace WebApi.Models;

public class Reader : User
{
    [JsonIgnore] public List<BookLoan> BookLoans { get; init; } = [];
    [JsonIgnore] public ReaderStatus Status { get; private set; } = ReaderStatus.ACTIVE;

    private Reader() { }

    public Reader(string name, string email, string phone) 
        : base(name, email, phone) { }

    public bool CanMakeBookLoan()
    {
        if (BookLoans.Any(x => x.IsLoanReturnLate()))
        {
            return false;
        }

        return BookLoans.Count < 3;
    }

    public void SetStatus(ReaderStatus status)
    {
        Status = status;
    }
}
