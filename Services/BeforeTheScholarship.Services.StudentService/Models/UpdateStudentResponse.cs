using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.Services.StudentService.Models;

public class UpdateStudentResponse
{

}

public class UpdateStudentResponseProfile : Profile
{
    public UpdateStudentResponseProfile()
    {
        CreateMap<StudentUser, UpdateStudentResponse>();
    }
}
