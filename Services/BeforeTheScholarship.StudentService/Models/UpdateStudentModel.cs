using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.StudentService;

public class UpdateStudentModel
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UpdateStudentModelProfile : Profile
{
    public UpdateStudentModelProfile()
    {
        CreateMap<UpdateStudentModel, StudentUser>();
    }
}