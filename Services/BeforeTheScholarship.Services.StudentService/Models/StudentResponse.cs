using AutoMapper;
using BeforeTheScholarship.Entities;
using System.Text.Json.Serialization;

namespace BeforeTheScholarship.Services.StudentService.Models;

public class StudentResponse
{
    [JsonPropertyName("id")] public Guid Id { get; set; }
    [JsonPropertyName("username")] public string UserName { get; set; }
    [JsonPropertyName("firstname")] public string FirstName { get; set; }
    [JsonPropertyName("lastname")] public string LastName { get; set; }
    [JsonPropertyName("phonenumber")] public string PhoneNumber { get; set; }
    [JsonPropertyName("email")] public string Email { get; set; }
    [JsonPropertyName("emailconfirmed")] public bool EmailConfirmed { get; set; }
}

public class StudentModelProfile : Profile
{
	public StudentModelProfile()
	{
		CreateMap<StudentUser, StudentResponse>();
	}
}