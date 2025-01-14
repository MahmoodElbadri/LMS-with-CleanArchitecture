using LMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Interfaces.Repositories;

public interface ILoanRepository
{
    Task<IEnumerable<Loan>> GetAllLoansAsync();
    Task<Loan> GetLoanByIdAsync(int id);
    Task<Loan> AddLoanAsync(Loan loan);
    Task UpdateLoanAsync(int id, Loan loan);
    Task DeleteLoanAsync(int id);
    Task<IEnumerable<Loan>> GetLoansDueTomorrowAsync();
    Task<IEnumerable<Loan>> GetLoansByUserIdAsync(int userId);
    Task<IEnumerable<Loan>> GetLoansByBookIdAsync(int bookId);
}
