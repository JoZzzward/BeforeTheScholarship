using AutoMapper;
using BeforeTheScholarship.Common.Validation;
using BeforeTheScholarship.Context;
using BeforeTheScholarship.Services.StudentService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BeforeTheScholarship.Services.StudentService;

public class StudentService : Manager, IStudentService
{
    private readonly IDbContextFactory<AppDbContext> _dbContext;
    private readonly ILogger<StudentService> _logger;
    private readonly IMapper _mapper;
    private readonly IModelValidator<UpdateStudentModel> _updateStudentModelValidator;

    public StudentService(
        IDbContextFactory<AppDbContext> dbContext,
        IMapper mapper,
        IModelValidator<UpdateStudentModel> updateStudentModelValidator,
        ILogger<StudentService> logger)
        : base (dbContext, logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _updateStudentModelValidator = updateStudentModelValidator;
        _logger = logger;
    }

    public async Task<IEnumerable<StudentModel>> GetStudents()
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var students = await context
            .StudentUsers
            .ToListAsync();

        var response = students.Select(_mapper.Map<StudentModel>)
            ?? new List<StudentModel>();

        _logger.LogInformation("--> Students(Count: {StudentsCount}) was returned successfully!", response.Count());

        return response;
    }

    public async Task<StudentModel> GetStudentById(Guid id)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var student = await FindStudentById(id);

        var response = _mapper.Map<StudentModel>(student);

        _logger.LogInformation("--> Student(Id: {StudentId}) was successfully returned!", id);

        return response;
    }

    public async Task<UpdateStudentResponse> UpdateStudent(Guid id, UpdateStudentModel model)
    {
        _updateStudentModelValidator.CheckValidation(model);

        using var context = await _dbContext.CreateDbContextAsync();

        var student = await FindStudentById(id);

        student = _mapper.Map(model, student);

        context.StudentUsers.Update(student!);
        context.SaveChanges();

        var response = _mapper.Map<UpdateStudentResponse>(student);

        _logger.LogInformation("--> Student(Id: {StudentId}) was successfully updated", id);

        return response;
    }

    public async Task<DeleteStudentResponse> DeleteStudent(Guid? id)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var student = await FindStudentById(id);

        context.StudentUsers.Remove(student);
        context.SaveChanges();

        var response = _mapper.Map<DeleteStudentResponse>(student);

        _logger.LogInformation("--> Student(Id: {StudentId}) was successfully removed", id);

        return response;
    }
}
