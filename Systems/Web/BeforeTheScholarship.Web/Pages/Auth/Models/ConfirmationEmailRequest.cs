using FluentValidation;

namespace BeforeTheScholarship.Web.Pages.Auth.Models;

public class ConfirmationEmailRequest
{
    public string Email { get; set; }
    public string Token { get; set; }
}

public class ConfirmationEmailRequestValidator : AbstractValidator<ConfirmationEmailRequest>
{
    public ConfirmationEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Incorrect email.")
            .MaximumLength(50).WithMessage("Email length must be less than 50");

        RuleFor(x => x.Token)
            .Length(260, 270).WithMessage("Invalid token.");
    }
}