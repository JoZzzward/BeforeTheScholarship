using FluentValidation;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class SendPasswordRecoveryModel
{
    public string Email { get; set; }
}

public class SendPasswordRecoveryModelValidator : AbstractValidator<SendPasswordRecoveryModel>
{
    public SendPasswordRecoveryModelValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Incorrect email.")
            .MaximumLength(50).WithMessage("Email length must be less than 50");
    }
}