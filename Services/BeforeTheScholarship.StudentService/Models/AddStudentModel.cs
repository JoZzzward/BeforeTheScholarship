using AutoMapper;

namespace BeforeTheScholarship.StudentService;

public class AddStudentModel
{
    public string UserName { get; set; }
	public string Password { get; set; }
}

public class AddStudentUserProfile : Profile
{
	public AddStudentUserProfile()
	{
		CreateMap<AddStudentModel, Entities.StudentUser>();
		CreateMap<AddStudentModel, StudentModel>();
    }
}