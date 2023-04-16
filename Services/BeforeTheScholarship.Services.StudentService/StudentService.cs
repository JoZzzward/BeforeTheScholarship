using AutoMapper;
using BeforeTheScholarship.Common.Validation;
using BeforeTheScholarship.Context;
using BeforeTheScholarship.Services.StudentService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BeforeTheScholarship.Services.StudentService;

public class StudentService : IStudentService
{
    private readonly IDbContextFactory<AppDbContext> _dbContext;
    private readonly ILogger<StudentService> _logger;
    private readonly IMapper _mapper;
    private readonly IModelValidator<UpdateStudentModel> _updateStudentModelValidator;

    public StudentService(
        IDbContextFactory<AppDbContext> dbContext,
        IMapper mapper,
        ILogger<StudentService> logger,
        IModelValidator<UpdateStudentModel> updateStudentModelValidator
        )
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _updateStudentModelValidator = updateStudentModelValidator;
        _logger = logger;
    }

    public async Task<IEnumerable<StudentResponse>> GetStudents()
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var students = await context
            .StudentUsers
            .ToListAsync();

        var response = students.Select(_mapper.Map<StudentResponse>);

        _logger.LogInformation("--> Students (Count: {StudentsCount}) was returned successfully!", response.Count());

        return response;
    }

    public async Task<StudentResponse?> GetStudentById(Guid id)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var student = await context
            .StudentUsers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student is null)
        {
            _logger.LogError("Student (Id: {StudentId}) was not found!", id);
            return null;
        }

        var response = _mapper.Map<StudentResponse>(student);

        _logger.LogInformation("--> Student (Id: {StudentId}) was successfully returned!", id);

         return response;
    }

    public async Task<UpdateStudentResponse?> UpdateStudent(Guid id, UpdateStudentModel model)
    {
        _updateStudentModelValidator.CheckValidation(model);

        using var context = await _dbContext.CreateDbContextAsync();

        var student = await context
            .StudentUsers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student is null)
        {
            _logger.LogError("Student (Id: {StudentId}) was not found!", id);
            return null;
        }

        student = _mapper.Map(model, student);

        context.StudentUsers.Update(student);
        context.SaveChanges();

        var response = _mapper.Map<UpdateStudentResponse>(student);

        _logger.LogInformation("--> Student (Id: {StudentId}) was successfully updated", id);

        return response;
    }

    public async Task<DeleteStudentResponse?> DeleteStudent(Guid? id)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var student = await context
            .StudentUsers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student is null)
        {
            _logger.LogError("Student (Id: {StudentId}) was not found!", id);
            return null;
        }

        context.StudentUsers.Remove(student);
        context.SaveChanges();

        var response = _mapper.Map<DeleteStudentResponse>(student);

        _logger.LogInformation("--> Student (Id: {StudentId}) was successfully removed", id);

        return response;
    }
}
