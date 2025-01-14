using LMS.Core.DTOs.RequestDTOs;
using LMS.Core.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Interfaces.Services;

public interface IBooksService
{
    Task<IEnumerable<BookResponse>> GetAllBooksAsync();
    Task<BookResponse> GetBookByIdAsync(int id);
    Task<BookResponse> AddBookAsync(BookAddRequest bookDto);
    Task<BookResponse> UpdateBookAsync(int id, BookAddRequest bookDto);
    Task<BookResponse> DeleteBookAsync(int id);
}
