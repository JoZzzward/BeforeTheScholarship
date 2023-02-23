namespace BeforeTheScholarship.API.Controllers.Models;

using AutoMapper;
using BeforeTheScholarship.Services.UserAccount;

public class UserAccountResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public string Error { get; set; }
}

public class UserAccountResponseProfile : Profile
{
    public UserAccountResponseProfile()
    {
        CreateMap<UserAccountModel, UserAccountResponse>();
    }
}
