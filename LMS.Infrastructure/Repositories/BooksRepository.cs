using LMS.Core.Interfaces.Repositories;
using LMS.Core.Models;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repositories;

public class BooksRepository : IBookRepository
{
    private readonly ILogger<BooksRepository> _logger;
    private readonly ApplicationDbContext _db;
    public BooksRepository(ApplicationDbContext db, ILogger<BooksRepository> logger)
    {
        _logger = logger;
        _db = db;
    }


    public async Task<Book> AddBookAsync(Book book)
    {
        try
        {
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();
            return book;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding a book");
            throw;
        }
    }

    public async Task<Book> DeleteBookAsync(int ID)
    {
        try
        {
            var book = await _db.Books.FindAsync(ID);
            if(book == null)
            {
                _logger.LogWarning($"Book with ID {ID} not found");
                throw new Exception("Book not found");
            }
            else
            {
                _db.Books.Remove(book);
                await _db.SaveChangesAsync();
                return book;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting a book");
            throw;
        }
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        try
        {
            return await _db.Books.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting all books");
            throw;
        }
    }

    public async Task<Book> GetBookByIDAsync(int ID)
    {
        try
        {
            //this is will not throw an exception to throw an exception in the controller 
            //return await _db.Books.Where(tmp => tmp.ID == ID).FirstOrDefaultAsync();
            return await _db.Books.FindAsync(ID);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting a book by ID");
            throw;
        }
    }

    public async Task<Book> UpdateBookAsync(int ID, Book book)
    {
        try
        {
            var existBook = await _db.Books.FindAsync(ID);
            if(book == null)
            {
                _logger.LogWarning($"Book with ID {ID} not found");
                throw new Exception("Book not found");
            }
            else
            {
                existBook.Author = book.Author;
                existBook.PublishDate = book.PublishDate;
                existBook.Title = book.Title;
                existBook.Loans = book.Loans;
                existBook.IsBorrowed = book.IsBorrowed;
                await _db.SaveChangesAsync();
                return existBook;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating a book");
            throw;
        }
        
    }
}
