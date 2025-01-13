using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.DTOs.RequestDTOs.Validators;

public class BookDtoValidator:AbstractValidator<BookAddRequest>
{
    public BookDtoValidator()
    {
        RuleFor(tmp=>tmp.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(50)
            .WithMessage("Title must not exceed 50 characters")
            .MinimumLength(3)
            .WithMessage("Title must not be less than 3 characters");

        RuleFor(tmp=>tmp.Author)
            .NotEmpty()
            .WithMessage("Author is required")
            .MaximumLength(50)
            .WithMessage("Author must not exceed 50 characters")
            .MinimumLength(3)
            .WithMessage("Author must not be less than 3 characters");

        RuleFor(tmp=>tmp.IsBorrowed)
            .NotEmpty()
            .WithMessage("IsBorrowed is required")
            .Must(tmp => tmp == false)
            .WithMessage("Book is already borrowed");

        RuleFor(tmp=>tmp.PublishDate)
            .NotEmpty()
            .WithMessage("PublishDate is required")
            .LessThan(DateTime.Now)
            .WithMessage("PublishDate must be in the past");
    }
}
