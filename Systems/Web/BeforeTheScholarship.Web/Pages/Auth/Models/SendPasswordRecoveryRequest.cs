using FluentValidation;

namespace BeforeTheScholarship.Web.Pages.Auth.Models;
public class SendPasswordRecoveryRequest
{
    public string Email { get; set; }
}

public class PasswordRecoveryMailRequestValidator : AbstractValidator<SendPasswordRecoveryRequest>
{
    public PasswordRecoveryMailRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Incorrect email.")
            .Length(1, 50).WithMessage("Email length must be less than 50");
    }
}