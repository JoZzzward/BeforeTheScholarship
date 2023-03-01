namespace BeforeTheScholarship.Api.Controllers.Account.Models;

using AutoMapper;
using BeforeTheScholarship.UserAccountService.Models;

public class RegisterUserAccountRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public class RegisterUserAccountRequestProfile : Profile
{
    public RegisterUserAccountRequestProfile()
    {
        CreateMap<RegisterUserAccountRequest, RegisterUserAccountModel>();
    }
}

