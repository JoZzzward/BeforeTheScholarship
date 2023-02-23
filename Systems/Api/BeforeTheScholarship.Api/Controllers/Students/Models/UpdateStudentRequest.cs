using AutoMapper;
using BeforeTheScholarship.StudentService;

namespace BeforeTheScholarship.Api.Controllers.Students;

public class UpdateStudentRequest
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UpdateStudentRequestProfile : Profile
{
    public UpdateStudentRequestProfile()
    {
        CreateMap<UpdateStudentRequest, UpdateStudentModel>();
    }
}