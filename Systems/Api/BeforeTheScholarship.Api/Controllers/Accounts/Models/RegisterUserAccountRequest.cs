using AutoMapper;
using BeforeTheScholarship.Services.UserAccountService.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BeforeTheScholarship.Api.Controllers.Accounts;

public class RegisterUserAccountRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public class RegisterUserAccountRequestValidator : AbstractValidator<RegisterUserAccountRequest>
{
    public RegisterUserAccountRequestValidator()
    {
        RuleFor(x => x.UserName)
            .MaximumLength(30).WithMessage("Username length must be less than 30")
            .NotEmpty().WithMessage("Username is required");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Incorrect email")
            .MaximumLength(50).WithMessage("Email length must be less than 50");

        // Checks if password was 8-30 symbols, contains minimum 1 lowercase letter and equals with ConfirmPassword field
        RuleFor(x => x.Password)
            .Must(x => new Regex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$").Matches(x).Any())
            .WithMessage("Password must be 8 symbols or more")
            .WithMessage("Password must have minimum 1 lowercase letter")
            .Equal(x => x.ConfirmPassword).WithMessage("Password and ConfirmPassword must be equals");
    }
}

public class RegisterUserAccountRequestProfile : Profile
{
    public RegisterUserAccountRequestProfile()
    {
        CreateMap<RegisterUserAccountRequest, RegisterUserAccountModel>();
    }
}

