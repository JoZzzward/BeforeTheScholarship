using AutoMapper;
using BeforeTheScholarship.Services.UserAccountService.Models;
using FluentValidation;

namespace BeforeTheScholarship.Api.Controllers.Accounts;

public class PasswordRecoveryMailRequest
{
    public string Email { get; set; }
}

public class PasswordRecoveryMailRequestValidator : AbstractValidator<PasswordRecoveryMailRequest>
{
    public PasswordRecoveryMailRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Incorrect email.")
            .Length(1, 50).WithMessage("Email length must be less than 50");
    }
}

public class PasswordRecoveryMailRequestProfile : Profile
{
	public PasswordRecoveryMailRequestProfile()
	{
		CreateMap<PasswordRecoveryMailRequest, PasswordRecoveryMailModel>();
	}
}