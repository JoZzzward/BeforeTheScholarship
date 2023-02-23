using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.Services.UserAccount;

public class UserAccountModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public string Error { get; set; }
    public IEnumerable<string> ErrorDescription { get; set; }
}

public class UserAccountModelProfile : Profile
{
    public UserAccountModelProfile()
    {
        CreateMap<StudentUser, UserAccountModel>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
            ;
    }
}
