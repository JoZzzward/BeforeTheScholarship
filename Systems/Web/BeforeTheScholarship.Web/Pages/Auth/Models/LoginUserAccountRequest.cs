using System.Text.RegularExpressions;
using FluentValidation;

namespace BeforeTheScholarship.Web.Pages.Auth.Models;

public class LoginUserAccountRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginUserAccountRequestValidator : AbstractValidator<LoginUserAccountRequest>
{
    public LoginUserAccountRequestValidator()
    {
        RuleFor(x => x.Email)
            .MaximumLength(50).WithMessage("Email length must be less than 50");

        // Checks if password was 8-30 symbols and contains minimum 1 lowercase letter 
        RuleFor(x => x.Password)
            .Must(x => new Regex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$").Matches(x).Any())
            .WithMessage("Password must be 8 symbols or more")
            .Must(x => new Regex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$").Matches(x).Any())
            .WithMessage("Password must have minimum 1 lowercase letter");
    }
}