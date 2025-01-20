using LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure entity properties
        modelBuilder.Entity<Book>()
            .Property(b => b.PublishDate)
            .HasColumnType("datetime2");

        modelBuilder.Entity<Loan>()
            .Property(l => l.ReturnDate)
            .HasColumnType("datetime2");

        // Configure relationships
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.BookID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Loan>()
            .HasOne(l => l.User)
            .WithMany(u => u.Loans)
            .HasForeignKey(l => l.UserID)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed data for Users
        var users = new List<User>
    {
        new User
        {
            ID = 1,
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "john.doe@example.com"
        },
        new User
        {
            ID = 2,
            FirstName = "Jane",
            LastName = "Smith",
            EmailAddress = "jane.smith@example.com"
        },
        new User
        {
            ID = 3,
            FirstName = "Alice",
            LastName = "Johnson",
            EmailAddress = "alice.johnson@example.com"
        }
    };
        modelBuilder.Entity<User>().HasData(users);

        // Seed data for Books
        var books = new List<Book>
    {
        new Book
        {
            ID = 1,
            Title = "The Great Gatsby",
            Author = "F. Scott Fitzgerald",
            PublishDate = new DateTime(1925, 4, 10),
            IsBorrowed = false
        },
        new Book
        {
            ID = 2,
            Title = "To Kill a Mockingbird",
            Author = "Harper Lee",
            PublishDate = new DateTime(1960, 7, 11),
            IsBorrowed = false
        },
        new Book
        {
            ID = 3,
            Title = "1984",
            Author = "George Orwell",
            PublishDate = new DateTime(1949, 6, 8),
            IsBorrowed = false
        }
    };
        modelBuilder.Entity<Book>().HasData(books);

        // Seed data for Loans
        var loans = new List<Loan>
    {
        new Loan
        {
            ID = 1,
            BookID = 1, // The Great Gatsby
            UserID = 1, // John Doe
            LoanDate = new DateTime(2023, 10, 1),
            DueDate = new DateTime(2023, 10, 15),
            ReturnDate = new DateTime(2023, 10, 10),
            IsReturned = true
        },
        new Loan
        {
            ID = 2,
            BookID = 2, // To Kill a Mockingbird
            UserID = 2, // Jane Smith
            LoanDate = new DateTime(2023, 10, 5),
            DueDate = new DateTime(2023, 10, 20),
            ReturnDate = new DateTime(2023, 10, 18),
            IsReturned = true
        },
        new Loan
        {
            ID = 3,
            BookID = 3, // 1984
            UserID = 3, // Alice Johnson
            LoanDate = new DateTime(2023, 10, 10),
            DueDate = new DateTime(2023, 10, 25),
            ReturnDate = DateTime.MinValue, // Not returned yet
            IsReturned = false
        }
    };
        modelBuilder.Entity<Loan>().HasData(loans);
    }

}

