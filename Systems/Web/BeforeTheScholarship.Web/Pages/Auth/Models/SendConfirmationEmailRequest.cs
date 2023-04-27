using FluentValidation;

namespace BeforeTheScholarship.Web.Pages.Auth.Models;
public class SendConfirmationEmailRequest
{
    public string Email { get; set; }
}

public class SendConfirmationEmailRequestValidator : AbstractValidator<SendConfirmationEmailRequest>
{
    public SendConfirmationEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Incorrect email.")
            .MaximumLength(50).WithMessage("Email length must be less than 50");
    }
}
