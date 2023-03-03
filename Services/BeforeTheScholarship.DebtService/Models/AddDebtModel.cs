using AutoMapper;
using BeforeTheScholarship.Entities;
using FluentValidation;

namespace BeforeTheScholarship.DebtService;

public class AddDebtModel
{
    public int StudentId { get; set; }  
    public decimal Borrowed { get; set; }
    public string Phone { get; set; }
    public string BorrowedFromWho { get; set; }
}

public class AddDebtModelValidator : AbstractValidator<AddDebtModel>
{
    public AddDebtModelValidator()
    {
        RuleFor(x => x.Borrowed).NotEmpty()
            .WithMessage("Borrowed money must be not empty.");

        RuleFor(x => x.Borrowed).GreaterThan(0)
           .WithMessage("Borrowed money must be greater than 0.");

        RuleFor(x => x.BorrowedFromWho)
            .MaximumLength(30)
            .NotEmpty() // Maybe be can be modified to Length(1, 30) thats all.
            .WithMessage("The value from who borrowed must be less than 30 and not empty.");

        RuleFor(x => x.Phone)
            .Length(10, 12)
            .NotEmpty() // Maybe be this function can be deleted (?)
            .WithMessage("Phone number length must be 10-12 numbers.");

        RuleFor(x => x.Phone)
            .Must(x => int.TryParse(x, out _))
            .WithMessage("Phone number must contain only numbers.");
    }
}

public class AddDebtModelProfile : Profile
{
	public AddDebtModelProfile()
	{
		CreateMap<AddDebtModel, Debts>();
    }
}
