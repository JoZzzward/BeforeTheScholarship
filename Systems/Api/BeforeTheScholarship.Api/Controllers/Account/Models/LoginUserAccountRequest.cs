using AutoMapper;
using BeforeTheScholarship.UserAccountService.Models;

namespace BeforeTheScholarship.Api.Controllers.Accounts;

public class LoginUserAccountRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
public class LoginUserAccountRequestProfile : Profile
{
    public LoginUserAccountRequestProfile()
    {
        CreateMap<LoginUserAccountRequest, LoginUserAccountModel>();
    }
}