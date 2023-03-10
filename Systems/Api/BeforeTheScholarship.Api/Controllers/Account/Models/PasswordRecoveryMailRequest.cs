using AutoMapper;
using BeforeTheScholarship.UserAccountService.Models;

namespace BeforeTheScholarship.Api.Controllers.Account.Models;

public class PasswordRecoveryMailRequest
{
    public string Email { get; set; }
}

public class PasswordRecoveryMailRequestProfile : Profile
{
	public PasswordRecoveryMailRequestProfile()
	{
		CreateMap<PasswordRecoveryMailRequest, SendPasswordRecoveryModel>();
	}
}