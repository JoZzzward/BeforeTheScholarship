using AutoMapper;

namespace BeforeTheScholarship.Api.Controllers.Students;

public class AddStudentRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class AddStudentRequestProfile : Profile
{
    public AddStudentRequestProfile()
    {
        CreateMap<AddStudentRequest, StudentService.AddStudentModel>();
    }
}
