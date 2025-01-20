using AutoMapper;
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

public class LoansRepository : ILoanRepository
{
    private readonly ILogger<LoansRepository> _logger;
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public LoansRepository(ILogger<LoansRepository> logger, ApplicationDbContext db, IMapper mapper)
    {
        this._logger = logger;
        this._db = db;
        this._mapper = mapper;
    }
    public async Task<Loan> AddLoanAsync(Loan loan)
    {
        //ensures a database transaction is explicitly started. This is particularly useful when multiple changes need to be atomic.
        using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            var book = await _db.Books.SingleOrDefaultAsync(tmp => tmp.ID == loan.BookID);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {loan.BookID} not found");
            }
            var user = await _db.Users.SingleOrDefaultAsync();
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {loan.UserID} not found");
            }
            _db.Loans.Add(loan);
            await _db.SaveChangesAsync();
            await transaction.CommitAsync();
            return loan;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteLoanAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Loan with ID {id} not found");
        }

        var loan = await GetLoanByIdAsync(id);
        if (loan == null)
        {
            throw new KeyNotFoundException($"Loan with ID {id} not found");
        }
        _db.Loans.Remove(loan);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Loan>> GetAllLoansAsync()
    {
        return await _db.Loans
            .Include(tmp => tmp.Book)
            .Include(tmp => tmp.User)
            .ToListAsync();
    }

    public async Task<Loan> GetLoanByIdAsync(int id)
    {
        if(id <= 0) {
            throw new ArgumentException($"Loan with ID {id} not found");
        }

        return await _db.Loans
            .Include(tmp => tmp.Book)
            .Include(tmp => tmp.User)
            .FirstOrDefaultAsync(tmp => tmp.ID == id);
    }

    public async Task<IEnumerable<Loan>> GetLoansByBookIdAsync(int bookId)
    {
        if(bookId <= 0) {
            throw new ArgumentException($"Book with ID {bookId} not found");
        }

        return await _db.Loans
            .Include(tmp => tmp.Book)
            .Include(tmp => tmp.User)
            .Where(tmp => tmp.BookID == bookId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Loan>> GetLoansByUserIdAsync(int userId)
    {
        if (userId <= 0)
        {
            throw new ArgumentException($"User with ID {userId} not found");
        }

        return await _db.Loans
            .Include(tmp => tmp.Book)
            .Include(tmp => tmp.User)
            .Where(tmp => tmp.UserID == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Loan>> GetLoansDueTomorrowAsync()
    {
        return await _db.Loans
            .Include(tmp => tmp.Book)
            .Include(tmp => tmp.User)
            .Where(tmp => tmp.DueDate == DateTime.Now.AddDays(1))
            .ToListAsync();
    }

    public async Task UpdateLoanAsync(int id, Loan loan)
    {
        using var transaction = await _db.Database.BeginTransactionAsync();

        try
        {
            var existingLoan = await _db.Loans.FirstOrDefaultAsync(tmp=>tmp.ID == id);
            if (existingLoan == null)
            {
                throw new KeyNotFoundException($"Loan with ID {id} does not exist.");
            }


            if (!existingLoan.IsReturned && loan.IsReturned)
            {
                var book = await _db.Books.FindAsync(existingLoan.BookID);
                if (book == null)
                {
                    throw new KeyNotFoundException($"Book with ID {existingLoan.BookID} does not exist.");
                }

                book.IsBorrowed = false;
            }

            _mapper.Map(loan, existingLoan);

            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Error updating the loan");
            throw;
        }
    }
}
