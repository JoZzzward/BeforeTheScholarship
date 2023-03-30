using FluentValidation;
using System.Text.RegularExpressions;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class LoginUserAccountModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginUserAccountModelValidator : AbstractValidator<LoginUserAccountModel>
{
    public LoginUserAccountModelValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Incorrect email.")
            .MaximumLength(50).WithMessage("Email length must be less than 50");

        RuleFor(x => x.Password)
            .Must(x => new Regex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$").Matches(x).Any())
            .WithMessage("Password must be 8 symbols or more")
            .WithMessage("Password must have minimum 1 lowercase letter");
    }
}