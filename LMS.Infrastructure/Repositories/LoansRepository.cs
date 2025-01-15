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
    private readonly ILogger<LoansRepository> logger;
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public LoansRepository(ILogger<LoansRepository> logger, ApplicationDbContext db,IMapper mapper)
    {
        this.logger = logger;
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
            if(book == null)
            {
                throw new KeyNotFoundException($"Book with ID {loan.BookID} not found");
            }
            var user = await _db.Users.SingleOrDefaultAsync()
        }
    }

    public Task DeleteLoanAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Loan>> GetAllLoansAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Loan> GetLoanByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Loan>> GetLoansByBookIdAsync(int bookId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Loan>> GetLoansByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Loan>> GetLoansDueTomorrowAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateLoanAsync(int id, Loan loan)
    {
        throw new NotImplementedException();
    }
}
