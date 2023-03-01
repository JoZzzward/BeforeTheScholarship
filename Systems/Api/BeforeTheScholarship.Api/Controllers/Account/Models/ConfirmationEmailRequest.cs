using AutoMapper;
using BeforeTheScholarship.UserAccountService.Models;

namespace BeforeTheScholarship.Api.Controllers.Account.Models;

public class ConfirmationEmailRequest
{
    public string Email { get; set; }
    public string Token { get; set; }
}

public class ConfirmationEmailProfile : Profile
{
    public ConfirmationEmailProfile()
    {
        CreateMap<ConfirmationEmailRequest, ConfirmationEmailModel>();
    }
}