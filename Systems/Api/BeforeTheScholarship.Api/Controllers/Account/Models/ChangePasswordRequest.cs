using AutoMapper;
using BeforeTheScholarship.UserAccountService.Models;

namespace BeforeTheScholarship.Api.Controllers.Accounts;

public class ChangePasswordRequest
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public string CurrentPassword { get; set; }
}

public class ChangePasswordRequestProfile : Profile
{
    public ChangePasswordRequestProfile()
    {
        CreateMap<ChangePasswordRequest, ChangePasswordModel>();
    }
}
