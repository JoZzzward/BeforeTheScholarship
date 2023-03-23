using AutoMapper;
using BeforeTheScholarship.Services.DebtService;
using FluentValidation;

namespace BeforeTheScholarship.Api.Controllers.Debts;

public class AddDebtRequest
{
    public Guid StudentId { get; set; }
    public decimal Borrowed { get; set; }
    public string Phone { get; set; }
    public string BorrowedFromWho { get; set; }
}

public class AddDebtRequestValidator : AbstractValidator<AddDebtRequest>
{
    public AddDebtRequestValidator()
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
            .ForEach(x =>
            {
                x.Must(char.IsDigit);
            }).WithMessage("Phone must contain only numbers.");
    }
}

public class AddDebtRequestProfile : Profile
{
    public AddDebtRequestProfile()
    {
        CreateMap<AddDebtRequest, CreateDebtModel>();
    }
}