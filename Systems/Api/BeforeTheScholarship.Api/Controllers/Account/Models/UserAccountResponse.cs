using AutoMapper;
using BeforeTheScholarship.Services.UserAccountService.Models;

namespace BeforeTheScholarship.Api.Controllers.Accounts;

public class UserAccountResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}

public class UserAccountResponseProfile : Profile
{
    public UserAccountResponseProfile()
    {
        CreateMap<UserAccountModel, UserAccountResponse>();
    }
}
