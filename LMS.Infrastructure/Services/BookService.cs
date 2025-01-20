using AutoMapper;
using LMS.Core.DTOs.RequestDTOs;
using LMS.Core.DTOs.ResponseDTOs;
using LMS.Core.Interfaces.Repositories;
using LMS.Core.Interfaces.Services;
using LMS.Core.Models;
using LMS.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Services;

public class BookService : IBooksService
{
    private readonly IBookRepository _bookRepo;
    private readonly ILogger<BookService> _logger;
    private readonly IMapper _mapper;

    public BookService(IBookRepository bookRepo, ILogger<BookService> logger, IMapper mapper)
    {
        _bookRepo = bookRepo;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<BookResponse> AddBookAsync(BookAddRequest bookDto)
    {
        if (bookDto == null)
        {
            throw new ArgumentNullException(nameof(bookDto), "Book cannot be null");
        }
        try
        {
            var book = _mapper.Map<Book>(bookDto);
            var result = await _bookRepo.AddBookAsync(book);
            var bookResponse = _mapper.Map<BookResponse>(result);
            return bookResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while adding a book with title {bookDto.Title} and author {bookDto.Author}");
            throw;
        }
    }

    public async Task<BookResponse> DeleteBookAsync(int id)
    {
        if (id < 0)
        {
            throw new ArgumentException("Book id must be greater than 0");
        }
        try
        {
            var book = await _bookRepo.DeleteBookAsync(id);
            var bookResponse = _mapper.Map<BookResponse>(book);
            return bookResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting book with id {id}");
            throw;
        }
    }

    public async Task<IEnumerable<BookResponse>> GetAllBooksAsync()
    {
        try
        {
            var books = await _bookRepo.GetAllBooksAsync();
            var bookResponses = _mapper.Map<List<BookResponse>>(books);
            return bookResponses;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting all books");
            throw;
        }
    }

    public async Task<BookResponse> GetBookByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Book id must be greater than 0");
        }
        try
        {
            var book = await _bookRepo.GetBookByIDAsync(id);
            var bookRes = _mapper.Map<BookResponse>(book);
            return bookRes;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while getting book with id {id}");
            throw;
        }
    }

    public async Task<BookResponse> UpdateBookAsync(int id, BookAddRequest bookDto)
    {
        if (bookDto == null)
        {
            throw new ArgumentNullException(nameof(bookDto), "Book cannot be null");
        }
        if (id <= 0)
        {
            throw new ArgumentException("Book id must be greater than 0");
        }
        try
        {
            var book = _mapper.Map<Book>(bookDto);
            await _bookRepo.UpdateBookAsync(id, book);
            var bookResponse = _mapper.Map<BookResponse>(book);
            return bookResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while updating book with id {id}");
            throw;
        }
    }
}
