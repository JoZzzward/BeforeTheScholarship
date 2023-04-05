using BeforeTheScholarship.Services.StudentService.Models;

namespace BeforeTheScholarship.Services.StudentService;

public interface IStudentService
{
    /// <summary>
    /// Returns a list of students
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<StudentResponse>> GetStudents();
    /// <summary>
    /// Returns <see cref="StudentResponse"/> with same <paramref name="id"/>
    /// </summary>
    Task<StudentResponse?> GetStudentById(Guid id);
    /// <summary>
    /// Updates a <see cref="StudentResponse"/> in database with the same <paramref name="id"/>
    /// </summary>
    Task<UpdateStudentResponse?> UpdateStudent(Guid id, UpdateStudentModel model);
    /// <summary>
    /// Removes a <see cref="StudentResponse"/> in database with the same <paramref name="id"/>.
    /// </summary>
    Task<DeleteStudentResponse?> DeleteStudent(Guid? id);
}