using FluentValidation;

namespace BeforeTheScholarship.UserAccountService.Models;

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
                .EmailAddress()
                .WithMessage("Incorrect email.")
                .MaximumLength(50).WithMessage("Email length must be less than 50");

        RuleFor(x => x.NewPassword)
                .MinimumLength(8).WithMessage("Minimum length is 8.")
                .Must(PasswordHasNumbers).WithMessage("Password must contain numbers.")
                .Must(PasswordHasLetters).WithMessage("Password must contain letters.");

        RuleFor(x => x.CurrentPassword)
                .MinimumLength(8).WithMessage("Minimum length is 8.")
                .Must(PasswordHasNumbers).WithMessage("Password must contain numbers.")
                .Must(PasswordHasLetters).WithMessage("Password must contain letters.");
    }

    protected bool PasswordHasNumbers(string password) => password.Any(x => char.IsDigit(x));

    protected bool PasswordHasLetters(string password) => password.Any(x => char.IsLetter(x));
}