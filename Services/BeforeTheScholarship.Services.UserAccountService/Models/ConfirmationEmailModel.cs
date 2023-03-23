using AutoMapper;
using FluentValidation;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class ConfirmationEmailModel
{
    public string Email { get; set; }
    public string Token { get; set; }
}

public class ConfirmationEmailModelValidator : AbstractValidator<ConfirmationEmailModel>
{
    public ConfirmationEmailModelValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Incorrect email.")
            .MaximumLength(50).WithMessage("Email length must be less than 50");

        RuleFor(x => x.Token)
            .MinimumLength(260).WithMessage("Token must have correct length")
            .MaximumLength(270).WithMessage("Token must have correct length");
    }
}

public class ConfirmationEmailModelProfile : Profile
{
    public ConfirmationEmailModelProfile()
    {
        CreateMap<ConfirmationEmailModel, ConfirmationEmailResponse>();
    }
}