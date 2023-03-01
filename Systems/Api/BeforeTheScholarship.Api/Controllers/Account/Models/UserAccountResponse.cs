namespace BeforeTheScholarship.Api.Controllers.Account.Models;

using AutoMapper;
using BeforeTheScholarship.UserAccountService.Models;

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
