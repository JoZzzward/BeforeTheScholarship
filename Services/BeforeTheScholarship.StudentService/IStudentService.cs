﻿
namespace BeforeTheScholarship.StudentService;

public interface IStudentService
{
    /// <summary>
    /// Returns a list of all students
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<StudentModel>> GetStudents();
    /// <summary>
    /// Returns <see cref="StudentModel"/> with same <paramref name="id"/>
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    Task<StudentModel> GetStudentById(int? id);
    /// <summary>
    /// Adds a new student to the database
    /// </summary>
    /// <param name="model">Limited <see cref="StudentModel"/> model that contains only initial information for <see cref="StudentModel"/></param>
    Task<StudentModel> CreateStudent(AddStudentModel model);
    /// <summary>
    /// Updates a <see cref="StudentModel"/> in database with the same <paramref name="id"/>
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    Task UpdateStudent(int id, UpdateStudentModel model);
    /// <summary>
    /// Removes a <see cref="StudentModel"/> in database with the same <paramref name="id"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    Task DeleteStudent(int? id);
}