using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class LoginUserAccountResponse
{
	public string Email { get; set; }
}

public class LoginUserAccountResponseProfile : Profile
{
	public LoginUserAccountResponseProfile()
	{
		CreateMap<LoginUserAccountModel, LoginUserAccountResponse>();
		CreateMap<StudentUser, LoginUserAccountResponse>();
	}
}
