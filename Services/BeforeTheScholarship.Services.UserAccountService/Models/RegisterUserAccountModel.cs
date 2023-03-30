using AutoMapper;
using BeforeTheScholarship.Entities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class RegisterUserAccountModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public class RegisterUserAccountModelValidator : AbstractValidator<RegisterUserAccountModel>
{
    public RegisterUserAccountModelValidator()
    {
        RuleFor(x => x.UserName)
            .MaximumLength(30).WithMessage("Username length must be less than 30")
            .NotEmpty().WithMessage("Username is required");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Incorrect email")
            .MaximumLength(50).WithMessage("Email length must be less than 50");

        RuleFor(x => x.Password)
            .Must(x => new Regex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$").Matches(x).Count() > 0)
            .WithMessage("Password must be 8 symbols or more")
            .WithMessage("Password must have minimum 1 lowercase letter")
            .Equal(x => x.ConfirmPassword).WithMessage("Password and ConfirmPassword must be equals");
    }
}

public class RegisterUserAccountModelProfile : Profile
{
    public RegisterUserAccountModelProfile()
    {
        CreateMap<RegisterUserAccountModel, StudentUser>()
            .ForSourceMember(dest => dest.Password, opt => opt.DoNotValidate())
            .ForSourceMember(dest => dest.ConfirmPassword, opt => opt.DoNotValidate());
    }
}
