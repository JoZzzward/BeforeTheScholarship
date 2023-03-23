using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class PasswordRecoveryResponse
{
    public string UserName { get; set; }
    public string Email { get; set; }
}

public class PasswordRecoveryResponseProfile : Profile 
{
    public PasswordRecoveryResponseProfile()
    {
        CreateMap<StudentUser, PasswordRecoveryResponse>();
        CreateMap<PasswordRecoveryMailModel, PasswordRecoveryResponse>();
    }
}