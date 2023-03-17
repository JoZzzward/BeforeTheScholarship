using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class RegisterUserAccountResponse
{
    public string UserId { get; set; }
    public string Email { get; set; }
}

public class RegisterUserAccountResponseProfile : Profile
{
    public RegisterUserAccountResponseProfile()
    {
        CreateMap<StudentUser, RegisterUserAccountResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.Id));
    }
}