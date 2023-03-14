using BeforeTheScholarship.Services.StudentService.Models;

namespace BeforeTheScholarship.Services.StudentService;

public interface IStudentService
{
    /// <summary>
    /// Returns a list of students
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<StudentModel>> GetStudents();
    /// <summary>
    /// Returns <see cref="StudentModel"/> with same <paramref name="id"/>
    /// </summary>
    Task<StudentModel> GetStudentById(Guid id);
    /// <summary>
    /// Updates a <see cref="StudentModel"/> in database with the same <paramref name="id"/>
    /// </summary>
    Task UpdateStudent(Guid id, UpdateStudentModel model);
    /// <summary>
    /// Removes a <see cref="StudentModel"/> in database with the same <paramref name="id"/>.
    /// </summary>
    Task DeleteStudent(Guid? id);
}