using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.Services.StudentService.Models;

public class DeleteStudentResponse
{
    public Guid? Id { get; set; }
}

public class DeleteStudentResponseProfile : Profile
{
	public DeleteStudentResponseProfile()
	{
		CreateMap<StudentUser, DeleteStudentResponse>();
	}
}
