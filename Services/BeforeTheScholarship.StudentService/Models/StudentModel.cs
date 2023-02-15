using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.StudentService;

public class StudentModel
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
}

public class StudentModelProfile : Profile
{
	public StudentModelProfile()
	{
		CreateMap<StudentUser, StudentModel>();
	}
}