using AutoMapper;
using BeforeTheScholarship.Common.Validation;
using BeforeTheScholarship.Context;
using Microsoft.EntityFrameworkCore;

namespace BeforeTheScholarship.StudentService;

public class StudentService : IStudentService
{
    private readonly IDbContextFactory<AppDbContext> _dbContext;
    private readonly IMapper _mapper;
    private readonly IModelValidator<UpdateStudentModel> _updateStudentModelValidator;

    public StudentService(
        IDbContextFactory<AppDbContext> dbContext, 
        IMapper mapper,
        IModelValidator<UpdateStudentModel> updateStudentModelValidator)
	{
        _dbContext = dbContext;
        _mapper = mapper;
        _updateStudentModelValidator = updateStudentModelValidator;
    }

    public async Task<IEnumerable<StudentModel>> GetStudents()
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var students = context
            .StudentUsers
            .AsQueryable();

        var data = (await students.ToListAsync()).Select(s => _mapper.Map<StudentModel>(s))
            ?? new List<StudentModel>();

        return data;
    }

    public async Task<StudentModel> GetStudentById(Guid id)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var student = await context
            .StudentUsers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student is null)
            throw new NullReferenceException($"Student({id}) was not found");

        var data = _mapper.Map<StudentModel>(student);

        return data;
    }

    public async Task UpdateStudent(Guid id, UpdateStudentModel model)
    {
        _updateStudentModelValidator.CheckValidation(model);

        using var context = await _dbContext.CreateDbContextAsync();

        var student = await context
            .StudentUsers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student is null)
            throw new NullReferenceException($"Student({id}) was not found");

        // Disable Email confirm field if Email was changed
        if (student.Email != model.Email)
        {
            student.EmailConfirmed = false;
            student.NormalizedEmail = model.Email.ToUpper();
        }

        // Disable PhoneNumber confirm field if Email was changed
        if (student.PhoneNumber != model.PhoneNumber) student.PhoneNumberConfirmed = false;

        // Change normalized UserName if current UserName was changed
        if (student.UserName != model.UserName) student.NormalizedUserName = model.UserName.ToUpper();

        student = _mapper.Map(model, student);

        context.StudentUsers.Update(student);
        context.SaveChanges();
    }

    public async Task DeleteStudent(Guid? id)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var student = await context.StudentUsers.FirstOrDefaultAsync(x => x.Id == id);

        if (student is null)
            throw new NullReferenceException($"Student({id}) was not found");

        context.StudentUsers.Remove(student);
        context.SaveChanges();
    }
}
