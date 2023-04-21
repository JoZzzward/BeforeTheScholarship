using AutoMapper;
using BeforeTheScholarship.Entities;
using System.Text.Json.Serialization;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class LoginUserAccountResponse
{
    [JsonPropertyName("email")] public string Email { get; set; }
    [JsonPropertyName("studentid")] public string StudentId { get; set; }
}

public class LoginUserAccountResponseProfile : Profile
{
	public LoginUserAccountResponseProfile()
	{
		CreateMap<LoginUserAccountModel, LoginUserAccountResponse>();
		CreateMap<StudentUser, LoginUserAccountResponse>()
            .ForMember(dest => dest.StudentId, opt => opt.MapFrom(x => x.Id));
	}
}
