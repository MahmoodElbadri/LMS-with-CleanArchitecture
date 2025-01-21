using LMS.Core.DTOs.RequestDTOs;
using LMS.Core.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Interfaces.Services;

public interface ILoansService
{
    Task<IEnumerable<LoanResponse>> GetAllLoansAsync();
    Task<LoanResponse?> GetLoanByIdAsync(int id);
    Task<LoanResponse> AddLoanAsync(LoanAddRequest loanDto);
    Task<LoanResponse> UpdateLoanAsync(int id, LoanAddRequest loanDto);
    Task DeleteLoanAsync(int id);
}
