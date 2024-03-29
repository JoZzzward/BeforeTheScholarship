﻿using AutoMapper;
using BeforeTheScholarship.Entities;
using FluentValidation;

namespace BeforeTheScholarship.Services.DebtService;

public class CreateDebtModel
{
    public Guid StudentId { get; set; }
    public decimal Borrowed { get; set; }
    public string Phone { get; set; }
    public string BorrowedFromWho { get; set; }
}

public class CreateDebtModelValidator : AbstractValidator<CreateDebtModel>
{
    public CreateDebtModelValidator()
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

public class CreateDebtModelProfile : Profile
{
	public CreateDebtModelProfile()
	{
		CreateMap<CreateDebtModel, Debts>();
    }
}
