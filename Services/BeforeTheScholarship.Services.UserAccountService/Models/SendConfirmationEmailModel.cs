using AutoMapper;
using FluentValidation;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class SendConfirmationEmailModel
{
    public string Email { get; set; }
}

public class SendConfirmationEmailModelValidator : AbstractValidator<SendConfirmationEmailModel>
{
    public SendConfirmationEmailModelValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Incorrect email.")
            .MaximumLength(50).WithMessage("Email length must be less than 50");
    }
}

public class SendConfirmationEmailModelProfile : Profile
{
    public SendConfirmationEmailModelProfile()
    {
        CreateMap<SendConfirmationEmailModel, SendConfirmationEmailResponse>();
    }
}
