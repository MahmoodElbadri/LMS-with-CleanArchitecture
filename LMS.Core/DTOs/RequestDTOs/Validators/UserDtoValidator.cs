using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.DTOs.RequestDTOs.Validators;

public class UserDtoValidator:AbstractValidator<UserAddRequest>
{
    public UserDtoValidator()
    {
        RuleFor(tmp=>tmp.FirstName)
            .NotEmpty()
            .WithMessage("First Name is required")
            .Length(3,50)
            .WithMessage("First Name must be between 3 and 50 characters");

        RuleFor(tmp=>tmp.LastName)
            .NotEmpty()
            .WithMessage("Last Name is required")
            .Length(3, 50)
            .WithMessage("Last Name must be between 3 and 50 characters");

        RuleFor(tmp=>tmp.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is not valid")
            .Length(10, 50)
            .WithMessage("Email must be between 10 and 50 characters");

    }
}
