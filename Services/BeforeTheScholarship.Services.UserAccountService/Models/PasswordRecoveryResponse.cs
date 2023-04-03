using AutoMapper;
using BeforeTheScholarship.Entities;
using System.Text.Json.Serialization;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class PasswordRecoveryResponse
{
    [JsonPropertyName("username")] public string UserName { get; set; }
    [JsonPropertyName("email")] public string Email { get; set; }
}

public class PasswordRecoveryResponseProfile : Profile 
{
    public PasswordRecoveryResponseProfile()
    {
        CreateMap<StudentUser, PasswordRecoveryResponse>();
        CreateMap<PasswordRecoveryMailModel, PasswordRecoveryResponse>();
    }
}