using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.DTOs.RequestDTOs.Validators;

public class LoanDtoValidator : AbstractValidator<LoanAddRequest>
{
    public LoanDtoValidator()
    {
        RuleFor(tmp => tmp.UserID)
            .NotEmpty()
            .WithMessage("User ID is required")
            .GreaterThanOrEqualTo(1)
            .WithMessage("User ID must be greater than 0");

        RuleFor(tmp => tmp.BookID)
            .NotEmpty()
            .WithMessage("Book ID is required")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Book ID must be greater than 0");

        RuleFor(tmp => tmp.LoanDate)
            .NotEmpty()
            .WithMessage("Loan Date is required")
            .GreaterThanOrEqualTo(DateTime.Now)
            .WithMessage("Loan Date must be greater than or equal to today");

        RuleFor(tmp => tmp.ReturnDate)
            .NotEmpty()
            .WithMessage("Return Date is required")
            .GreaterThanOrEqualTo(tmp => tmp.LoanDate)
            .WithMessage("Return Date must be greater than or equal to Loan Date")
            .GreaterThanOrEqualTo(DateTime.Now)
            .WithMessage("Return Date must be greater than or equal to today");

        RuleFor(tmp => tmp.DueDate)
            .NotEmpty()
            .WithMessage("Due Date is required")
            .GreaterThanOrEqualTo(tmp => tmp.ReturnDate)
            .WithMessage("Due Date must be greater than or equal to Return Date");
    }
}
