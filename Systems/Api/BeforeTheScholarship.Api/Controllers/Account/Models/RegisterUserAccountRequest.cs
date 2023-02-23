namespace BeforeTheScholarship.API.Controllers.Models;

using AutoMapper;
using BeforeTheScholarship.Services.UserAccount;

public class RegisterUserAccountRequest
{
    public string FirstName { get; set; }
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

