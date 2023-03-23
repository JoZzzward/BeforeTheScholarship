using AutoMapper;
using BeforeTheScholarship.Services.StudentService.Models;
using BeforeTheScholarship.Services.UserAccountService.Models;
using FluentValidation;

namespace BeforeTheScholarship.Api.Controllers.Students;

public class UpdateStudentRequest
{
    public string UserName { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty; 
    public string Email { get; set; }
}

public class UpdateStudentRequestValidator : AbstractValidator<UpdateStudentRequest>
{
    public UpdateStudentRequestValidator()
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
            .ForEach(x =>
            {
                x.Must(char.IsDigit);
            }).WithMessage("Phone must contain only numbers.");
    }
}

public class UpdateStudentRequestProfile : Profile
{
    public UpdateStudentRequestProfile()
    {
        CreateMap<UpdateStudentRequest, UpdateStudentModel>();
    }
}