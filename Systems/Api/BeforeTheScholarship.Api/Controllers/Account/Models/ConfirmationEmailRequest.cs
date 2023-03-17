using AutoMapper;
using BeforeTheScholarship.Services.UserAccountService.Models;

namespace BeforeTheScholarship.Api.Controllers.Accounts;

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