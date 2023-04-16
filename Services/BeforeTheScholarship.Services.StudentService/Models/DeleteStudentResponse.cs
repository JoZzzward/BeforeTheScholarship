using System.Text.Json.Serialization;
using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.Services.StudentService.Models;

public class DeleteStudentResponse
{
    [JsonPropertyName("id")] public Guid? Id { get; set; }
}

public class DeleteStudentResponseProfile : Profile
{
	public DeleteStudentResponseProfile()
	{
		CreateMap<StudentUser, DeleteStudentResponse>();
	}
}
