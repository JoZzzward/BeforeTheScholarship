using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class UserAccountModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}

public class UserAccountModelProfile : Profile
{
    public UserAccountModelProfile()
    {
        CreateMap<StudentUser, UserAccountModel>();
    }
}
