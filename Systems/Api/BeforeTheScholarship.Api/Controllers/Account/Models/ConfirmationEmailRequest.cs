using AutoMapper;
using BeforeTheScholarship.Services.UserAccount.Models;

namespace BeforeTheScholarship.Api.Controllers.Accounts.Models;

public class ConfirmationEmailRequest
{
    public string Email { get; set; }
    public string Token { get; set; }
}

public class ConfirmationEmailProfile : Profile
{
    public ConfirmationEmailProfile()
    {
        CreateMap<ConfirmationEmailRequest, ConfirmationEmail>();
    }
}