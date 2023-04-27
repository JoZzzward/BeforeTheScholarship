using System.Text.Json.Serialization;
using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class RegisterUserAccountResponse
{
    [JsonPropertyName("userid")] public string? UserId { get; set; }
    [JsonPropertyName("email")] public string? Email { get; set; }
    [JsonPropertyName ("error")] public string Error { get; set; }
}

public class RegisterUserAccountResponseProfile : Profile
{
    public RegisterUserAccountResponseProfile()
    {
        CreateMap<StudentUser, RegisterUserAccountResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.Id));
    }
}