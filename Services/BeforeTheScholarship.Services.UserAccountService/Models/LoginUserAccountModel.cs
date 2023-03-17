using FluentValidation;

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
            .EmailAddress()
            .WithMessage("Incorrect email.")
            .MaximumLength(50).WithMessage("Email length must be less than 50");

        RuleFor(x => x.Password)
            .MinimumLength(8).WithMessage("Minimum length is 8.")
            .Must(PasswordHasNumbers).WithMessage("Password must contain numbers.")
            .Must(PasswordHasLetters).WithMessage("Password must contain letters.");
    }

    protected bool PasswordHasNumbers(string password) => password.Any(x => char.IsDigit(x));

    protected bool PasswordHasLetters(string password) => password.Any(x => char.IsLetter(x));
}