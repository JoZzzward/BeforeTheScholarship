using AutoMapper;
using BeforeTheScholarship.Context;
using BeforeTheScholarship.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeforeTheScholarship.StudentService;

public class StudentService : IStudentService
{
    private readonly IDbContextFactory<AppDbContext> _dbContext;
    private readonly IMapper _mapper;

    public StudentService(
        IDbContextFactory<AppDbContext> dbContext, 
        IMapper mapper)
	{
        _dbContext = dbContext;
        _mapper = mapper;
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

    public async Task<StudentModel> CreateStudent(AddStudentModel model)
    {
        //TODO: Add student with IS4 by usermanager and other service
        using var context = await _dbContext.CreateDbContextAsync();

        var data = _mapper.Map<StudentUser>(model);

        await context.StudentUsers.AddAsync(data);
        context.SaveChanges();

        return _mapper.Map<StudentModel>(model);
    }
 
    public async Task UpdateStudent(Guid id, UpdateStudentModel model)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var student = await context
            .StudentUsers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student is null)
            throw new NullReferenceException($"Student({id}) was not found");

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
