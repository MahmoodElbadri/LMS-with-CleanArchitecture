using AutoMapper;
using LMS.Core.DTOs.RequestDTOs;
using LMS.Core.DTOs.ResponseDTOs;
using LMS.Core.Interfaces.Repositories;
using LMS.Core.Interfaces.Services;
using LMS.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Services;

public class LoansService : ILoansService
{
    private readonly ILoanRepository _loanRepo;
    private readonly ILogger<LoansService> _logger;
    private readonly IMapper _mapper;

    public LoansService(ILoanRepository loanRepo, ILogger<LoansService> logger, IMapper mapper)
    {
        this._loanRepo = loanRepo;
        this._logger = logger;
        this._mapper = mapper;
    }
    public async Task<LoanResponse> AddLoanAsync(LoanAddRequest loanDto)
    {
        if (loanDto == null) throw new ArgumentNullException(nameof(loanDto));
        try
        {
            var loan = _mapper.Map<Loan>(loanDto);
            await _loanRepo.AddLoanAsync(loan);
            var loanResponse = _mapper.Map<LoanResponse>(loanDto);
            return loanResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error adding loan to that book {loanDto.BookID} and user {loanDto.UserID}");
            throw;
        }
    }

    public async Task DeleteLoanAsync(int id)
    {
        if (id < 0)
        {
            throw new ArgumentException($"Argument should be greater than {id}");
        }
        try
        {
            await _loanRepo.DeleteLoanAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting loan with id {id}");
            throw;
        }
    }

    public async Task<IEnumerable<LoanResponse>> GetAllLoansAsync()
    {
        try
        {
            var loans = await _loanRepo.GetAllLoansAsync();
            var loansResponse = _mapper.Map<IEnumerable<LoanResponse>>(loans);
            return loansResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting all loans");
            throw;
        }
    }


    public async Task<LoanResponse?> GetLoanByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Argument should be greater than {id}");
        }
        try
        {
            var loan = await _loanRepo.GetLoanByIdAsync(id);

            if (loan == null)
            {
                return null;
            }
            var loanReponse = _mapper.Map<LoanResponse>(loan);
            return loanReponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while getting loan with id {id}");
            throw;
        }
    }

    public async Task UpdateLoanAsync(int id, LoanAddRequest loanDto)

    public async Task<LoanResponse> UpdateLoanAsync(int id, LoanAddRequest loanDto)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Argument should be greater than {id}");
        }
        if (loanDto == null) throw new ArgumentNullException(nameof(loanDto));
        try
        {
            var loan = _mapper.Map<Loan>(loanDto);
            loan.ID = id;
            await _loanRepo.UpdateLoanAsync(id, loan);
            loan = await _loanRepo.UpdateLoanAsync(id, loan);
            var loanResponse = _mapper.Map<LoanResponse>(loan);
            return loanResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while updating loan with id {id}");
            throw;
        }
    }

}
