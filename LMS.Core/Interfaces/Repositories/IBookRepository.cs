using LMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Interfaces.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIDAsync(int ID);
    Task<Book> AddBookAsync(Book book);
    Task<Book> UpdateBookAsync(int ID, Book book);
    Task<Book> DeleteBookAsync(int ID);
}
