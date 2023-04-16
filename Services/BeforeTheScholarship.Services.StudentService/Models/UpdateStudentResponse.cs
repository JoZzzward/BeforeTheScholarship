using AutoMapper;
using BeforeTheScholarship.Entities;
using System.Text.Json.Serialization;

namespace BeforeTheScholarship.Services.StudentService.Models;

public class UpdateStudentResponse
{
    [JsonPropertyName("id")] public Guid? Id { get; set; }
}

public class UpdateStudentResponseProfile : Profile
{
    public UpdateStudentResponseProfile()
    {
        CreateMap<StudentUser, UpdateStudentResponse>();
    }
}
