using AutoMapper;
using BeforeTheScholarship.Services.UserAccountService.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BeforeTheScholarship.Api.Controllers.Accounts;

public class LoginUserAccountRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginUserAccountRequestValidator : AbstractValidator<LoginUserAccountRequest>
{
    public LoginUserAccountRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Incorrect email.")
            .MaximumLength(50).WithMessage("Email length must be less than 50");

        // Checks if password was 8-30 symbols and contains minimum 1 lowercase letter 
        RuleFor(x => x.Password)
            .Must(x => new Regex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$").Matches(x).Any())
            .WithMessage("Password must be 8 symbols or more")
            .WithMessage("Password must have minimum 1 lowercase letter");
    }
}

public class LoginUserAccountRequestProfile : Profile
{
    public LoginUserAccountRequestProfile()
    {
        CreateMap<LoginUserAccountRequest, LoginUserAccountModel>();
    }
}