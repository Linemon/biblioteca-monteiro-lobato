using Microsoft.EntityFrameworkCore;
using WebApi.Models;
namespace WebApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<BookLoan> BookLoans { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Reader> Readers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BookLoan>()
            .HasOne(bl => bl.Book)
            .WithOne(b => b.BookLoan)
            .HasForeignKey<BookLoan>(bl => bl.BookId);

        modelBuilder.Entity<BookLoan>()
            .HasOne(bl => bl.Reader)
            .WithMany(r => r.BookLoans)
            .HasForeignKey(bl => bl.ReaderId);

        modelBuilder.Entity<BookLoan>()
            .HasOne(bl => bl.Employee)
            .WithMany(e => e.RegisteredBookLoans)
            .HasForeignKey(bl => bl.EmployeeId);
    }
}
