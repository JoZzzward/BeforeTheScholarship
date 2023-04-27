using System.Text.RegularExpressions;
using FluentValidation;

namespace BeforeTheScholarship.Web.Pages.Auth.Models;

public class ChangePasswordRequest
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public string CurrentPassword { get; set; }
}

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email must be not empty")
            .EmailAddress().WithMessage("Incorrect email.")
            .MaximumLength(50).WithMessage("Email length must be less than 50");

        // Checks if new password was 8-30 symbols and contains minimum 1 lowercase letter 
        RuleFor(x => x.NewPassword)
            .Must(x => new Regex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$").Matches(x).Any())
            .WithMessage("Password must be 8 symbols or more")
            .Must(x => new Regex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$").Matches(x).Any())
            .WithMessage("Password must have minimum 1 lowercase letter");


        // Checks if current password was 8-30 symbols and contains minimum 1 lowercase letter 
        RuleFor(x => x.CurrentPassword)
            .Must(x => new Regex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$").Matches(x).Any())
            .WithMessage("Password must be 8 symbols or more")
            .Must(x => new Regex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$").Matches(x).Any())
            .WithMessage("Password must have minimum 1 lowercase letter");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue =>
        async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ChangePasswordRequest>
                .CreateWithOptions(
                    (ChangePasswordRequest)model,
                    x => x.IncludeProperties(propertyName)));

            return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
        };
}