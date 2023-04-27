using FluentValidation;

namespace BeforeTheScholarship.Web.Pages.Debts.Models
{
    public class CreateDebtRequest
    {
        public Guid StudentId { get; set; }
        public decimal Borrowed { get; set; }
        public string Phone { get; set; }
        public string BorrowedFromWho { get; set; }
    }

    public class CreateDebtRequestValidator : AbstractValidator<CreateDebtRequest>
    {
        public CreateDebtRequestValidator()
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

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue =>
                async (model, propertyName) =>
                {
                    var result = await ValidateAsync(ValidationContext<CreateDebtRequest>
                        .CreateWithOptions(
                            (CreateDebtRequest)model,
                            x => x.IncludeProperties(propertyName)));

                    return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
                };
    }
}
