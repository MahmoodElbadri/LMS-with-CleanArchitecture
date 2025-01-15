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
        /*modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(tmp => tmp.PublishDate)
            .HasColumnType("timestamp without time zone");
        });*/

        modelBuilder.Entity<Book>()
            .Property(tmp => tmp.PublishDate)
            .HasColumnType("datetime2");

        modelBuilder.Entity<Loan>()
            .Property(tmp => tmp.ReturnDate)
            .HasColumnType("datetime2");

        modelBuilder.Entity<Loan>()
            .HasOne(tmp=>tmp.Book)
            .WithMany(tmp=>tmp.Loans)
            .HasForeignKey(tmp=>tmp.BookID)
            .OnDelete(DeleteBehavior.Restrict);


        //this is redundant code since it describes the same relationship between Book and Loan
        //modelBuilder.Entity<Book>()
        //    .HasMany(tmp => tmp.Loans)
        //    .WithOne(tmp => tmp.Book)
        //    .HasForeignKey(tmp => tmp.BookID)
        //    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Loan>()
            .HasOne(tmp=>tmp.User)
            .WithMany(tmp=>tmp.Loans)
            .HasForeignKey(tmp=>tmp.UserID)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
