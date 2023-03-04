using AutoMapper;
using BeforeTheScholarship.Entities;
using FluentValidation;

namespace BeforeTheScholarship.StudentService;

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

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Incorrect email.")
            .MaximumLength(50).WithMessage("Email length must be less than 50.");

        RuleFor(x => x.PhoneNumber)
            .ForEach(x =>
            {
                x.Must(x => char.IsDigit(x));
            }).WithMessage("Phone must contain only numbers.");
    }
}

public class UpdateStudentModelProfile : Profile
{
    public UpdateStudentModelProfile()
    {
        CreateMap<UpdateStudentModel, StudentUser>();
    }
}