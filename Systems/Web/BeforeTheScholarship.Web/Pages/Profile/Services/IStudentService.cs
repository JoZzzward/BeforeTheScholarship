using BeforeTheScholarship.Web.Pages.Profile.Models;

namespace BeforeTheScholarship.Web.Pages.Profile.Services
{
    public interface IStudentService
    {
        Task<StudentUserResponse?> GetStudentUser();
        Task<T> UpdateStudentById<T>(UpdateStudentUserRequest data);
        Task<T> DeleteStudentById<T>();
    }

}
