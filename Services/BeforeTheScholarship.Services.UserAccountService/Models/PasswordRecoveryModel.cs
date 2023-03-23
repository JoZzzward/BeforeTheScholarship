using FluentValidation;
using System.Text.RegularExpressions;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class PasswordRecoveryModel
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string NewPassword { get; set; }
}

public class PasswordRecoveryModelValidator : AbstractValidator<PasswordRecoveryModel>
{
    public PasswordRecoveryModelValidator()
    {
        RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Incorrect email.")
                .MaximumLength(50).WithMessage("Email length must be less than 50");

        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage("Email token must be not empty");

        RuleFor(x => x.NewPassword)
            .Must(x => new Regex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$").Matches(x).Count() > 0)
            .WithMessage("Password must be 8 symbols or more")
            .WithMessage("Password must have minimum 1 lowercase letter");
    }
}