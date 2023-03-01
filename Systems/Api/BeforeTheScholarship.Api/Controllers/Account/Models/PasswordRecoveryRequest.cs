using AutoMapper;
using BeforeTheScholarship.UserAccountService.Models;

namespace BeforeTheScholarship.Api.Controllers.Account.Models;

public class PasswordRecoveryRequest
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string NewPassword { get; set; }
}

public class PasswordRecoveryRequestProfile : Profile
{
    public PasswordRecoveryRequestProfile()
    {
        CreateMap<PasswordRecoveryRequest, PasswordRecoveryModel>(); 
        CreateMap<PasswordRecoveryRequest, SendPasswordRecoveryModel>(); 
    }
}
