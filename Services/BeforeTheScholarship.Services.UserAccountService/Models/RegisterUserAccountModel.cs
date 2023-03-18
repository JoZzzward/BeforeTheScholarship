using AutoMapper;
using BeforeTheScholarship.Entities;
using FluentValidation;

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
            .MaximumLength(30).WithMessage("Username length must be less than 30");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Incorrect email.")
            .MaximumLength(50).WithMessage("Email length must be less than 50");

        RuleFor(x => x.Password)
            .MinimumLength(8).WithMessage("Minimum length is 8.")
            .Equal(x => x.ConfirmPassword).WithMessage("Password and ConfirmPassword must be equals.")
            .Must(PasswordHasNumbers).WithMessage("Password must contain numbers.")
            .Must(PasswordHasLetters).WithMessage("Password must contain letters.");
    }

    protected bool PasswordHasNumbers(string password) => password.Any(x => char.IsDigit(x));

    protected bool PasswordHasLetters(string password) => password.Any(x => char.IsLetter(x));
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