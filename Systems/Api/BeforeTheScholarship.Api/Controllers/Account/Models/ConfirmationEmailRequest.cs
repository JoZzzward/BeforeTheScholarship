using AutoMapper;
using BeforeTheScholarship.Services.UserAccountService.Models;
using FluentValidation;

namespace BeforeTheScholarship.Api.Controllers.Accounts;

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
            .Length(260, 270).WithMessage("Token must have correct length");
    }
}

public class ConfirmationEmailProfile : Profile
{
    public ConfirmationEmailProfile()
    {
        CreateMap<ConfirmationEmailRequest, ConfirmationEmailModel>();
    }
}