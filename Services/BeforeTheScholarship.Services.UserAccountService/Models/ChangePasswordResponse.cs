using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class ChangePasswordResponse
{
    public string UserName { get; set; }
    public string Email { get; set; }
}

public class ChangePasswordResponseProfile : Profile
{
    public ChangePasswordResponseProfile()
    {
        CreateMap<StudentUser, ChangePasswordResponse>();
    }
}
