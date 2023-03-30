using AutoMapper;
using BeforeTheScholarship.Services.UserAccountService.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BeforeTheScholarship.Api.Controllers.Accounts;

public class ChangePasswordRequest
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public string CurrentPassword { get; set; }
}

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage("Email must be not empty")
                .EmailAddress()
                    .WithMessage("Incorrect email.")
                .MaximumLength(50)
                    .WithMessage("Email length must be less than 50");

        // Checks if password was 8-30 symbols and contains minimum 1 lowercase letter 
        RuleFor(x => x.NewPassword)
            .Must(x => new Regex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$").Matches(x).Any())
            .WithMessage("Password must be 8 symbols or more")
            .WithMessage("Password must have minimum 1 lowercase letter");
    }
}
public class ChangePasswordRequestProfile : Profile
{
    public ChangePasswordRequestProfile()
    {
        CreateMap<ChangePasswordRequest, ChangePasswordModel>();
    }
}
