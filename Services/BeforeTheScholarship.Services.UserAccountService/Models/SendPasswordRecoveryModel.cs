using FluentValidation;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class SendPasswordRecoveryModel
{
    public string Email { get; set; }
}

public class PasswordRecoveryMailModelValidator : AbstractValidator<SendPasswordRecoveryModel>
{
    public PasswordRecoveryMailModelValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Incorrect email.")
            .MaximumLength(50).WithMessage("Email length must be less than 50");
    }
}