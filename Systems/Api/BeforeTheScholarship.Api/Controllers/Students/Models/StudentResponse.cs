using AutoMapper;
using BeforeTheScholarship.Services.StudentService.Models;

namespace BeforeTheScholarship.Api.Controllers.Students;

public class StudentResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";
}

public class StudentResponseProfile : Profile
{
    public StudentResponseProfile()
    {
        CreateMap<StudentModel, StudentResponse>();
        CreateMap<StudentResponse, StudentModel>();
    }
}