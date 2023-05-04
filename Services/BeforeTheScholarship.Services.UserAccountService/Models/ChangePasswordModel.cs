using FluentValidation;
using System.Text.RegularExpressions;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class ChangePasswordModel
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public string CurrentPassword { get; set; }
}

public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
{
    public ChangePasswordModelValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Incorrect email.")
            .MaximumLength(50).WithMessage("Email length must be less than 50");

        RuleFor(x => x.NewPassword)
            .Must(x => new Regex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$").Matches(x).Count() > 0)
            .WithMessage("Password must be 8 symbols or more")
            .Must(x => new Regex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$").Matches(x).Count() > 0)
            .WithMessage("Password must have minimum 1 lowercase letter");
    }
}