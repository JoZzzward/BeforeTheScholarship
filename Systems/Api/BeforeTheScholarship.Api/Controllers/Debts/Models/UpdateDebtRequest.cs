﻿using AutoMapper;
using BeforeTheScholarship.Services.DebtService;
using FluentValidation;

namespace BeforeTheScholarship.Api.Controllers.Debts;

public class UpdateDebtRequest
{
    public decimal Borrowed { get; set; }
    public string Phone { get; set; }
    public string BorrowedFromWho { get; set; }
    public DateTimeOffset WhenToPayback { get; set; }
}

public class UpdateDebtRequestValidator : AbstractValidator<UpdateDebtRequest>
{
    public UpdateDebtRequestValidator()
    {
        RuleFor(x => x.Borrowed).NotEmpty()
            .WithMessage("Borrowed money must be not empty.");
        RuleFor(x => x.Borrowed).GreaterThan(0)
           .WithMessage("Borrowed money must be greater than 0.");

        RuleFor(x => x.BorrowedFromWho)
            .MaximumLength(30)
            .NotEmpty()
            .WithMessage("The value from who borrowed must be less than 30 and not empty.");

        RuleFor(x => x.Phone)
            .MaximumLength(12)
            .WithMessage("Phone number length must be less than 12 numbers.");

        RuleFor(x => x.Phone)
            .Must(CorrectPhone)
            .WithMessage("Phone number must contain only numbers.");
    }

    // Check is Phone field contains only numbers. Returns true if null or empty
    private static bool CorrectPhone(string number)
        => string.IsNullOrEmpty(number) || long.TryParse(number, out _);
}

public class UpdateDebtsRequestProfile : Profile
{
    public UpdateDebtsRequestProfile()
    {
        CreateMap<UpdateDebtRequest, UpdateDebtModel>();
    }
}