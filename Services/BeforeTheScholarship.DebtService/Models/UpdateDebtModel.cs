using AutoMapper;
using BeforeTheScholarship.Entities;
using FluentValidation;

namespace BeforeTheScholarship.DebtService;

public class UpdateDebtModel
{
    public decimal Borrowed { get; set; }
    public string Phone { get; set; }
    public string BorrowedFromWho { get; set; }
    public DateTime WhenToPayback { get; set; }
}

public class UpdateDebtModelValidator : AbstractValidator<UpdateDebtModel>
{
    public UpdateDebtModelValidator()
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
            .Length(10, 12)
            .NotEmpty() // Maybe be this function can be deleted (?)
            .WithMessage("Phone number length must be 10-12 numbers.");
        RuleFor(x => x.Phone)
            .Must(x => int.TryParse(x, out _))
            .WithMessage("Phone number must contain only numbers.");

        RuleFor(x => x.WhenToPayback).Must(DateSettings).NotEmpty();
    }

    // Checks if the payback date is more than 1 day from current moment.
    protected bool DateSettings(DateTime date) => date >= DateTime.Now.ToLocalTime().AddDays(1);
}

public class UpdateDebtModelProfile : Profile
{
	public UpdateDebtModelProfile()
	{
		CreateMap<UpdateDebtModel, Debts>();
		CreateMap<DebtModel, UpdateDebtModel>();
    }
}
