using AutoMapper;
using BeforeTheScholarship.Entities;
using System.Text.Json.Serialization;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class ChangePasswordResponse
{
    [JsonPropertyName("username")] public string UserName { get; set; }
    [JsonPropertyName("email")] public string Email { get; set; }
}

public class ChangePasswordResponseProfile : Profile
{
    public ChangePasswordResponseProfile()
    {
        CreateMap<StudentUser, ChangePasswordResponse>();
    }
}
