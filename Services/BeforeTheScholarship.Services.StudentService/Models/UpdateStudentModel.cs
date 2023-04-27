using AutoMapper;
using BeforeTheScholarship.Entities;
using FluentValidation;

namespace BeforeTheScholarship.Services.StudentService.Models;

public class UpdateStudentModel
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}

public class UpdateStudentmodelValidator : AbstractValidator<UpdateStudentModel>
{
    public UpdateStudentmodelValidator()
    {
        RuleFor(x => x.UserName)
            .MaximumLength(30).WithMessage("Username length must be less than 30.");

        RuleFor(x => x.FirstName)
            .MaximumLength(30).WithMessage("Firstname length must be less than 30.");

        RuleFor(x => x.LastName)
            .MaximumLength(30).WithMessage("Lastname length must be less than 30.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Incorrect email.")
            .MaximumLength(50).WithMessage("Email length must be less than 50.");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(12)
            .WithMessage("Phone number length must be less than 12 numbers.");

        RuleFor(x => x.PhoneNumber)
            .Must(CorrectPhone)
            .WithMessage("Phone number must contain only numbers.");
    }

    // Check is Phone field contains only numbers. Returns true if null or empty
    private static bool CorrectPhone(string number)
        => string.IsNullOrEmpty(number) || long.TryParse(number, out _);
}

public class UpdateStudentModelProfile : Profile
{
    public UpdateStudentModelProfile()
    {
        CreateMap<UpdateStudentModel, StudentUser>()
            .ForMember(x => x.NormalizedUserName, opt => opt.MapFrom(x => x.UserName.ToUpper()))
            .ForMember(x => x.NormalizedEmail, opt => opt.MapFrom(x => x.Email.ToUpper()));
    }
}