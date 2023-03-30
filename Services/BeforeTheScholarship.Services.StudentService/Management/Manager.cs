using BeforeTheScholarship.Context;
using BeforeTheScholarship.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BeforeTheScholarship.Services.StudentService;

public class Manager
{
    private readonly IDbContextFactory<AppDbContext> _dbContext;
    private readonly ILogger<StudentService> _logger;
    public Manager(IDbContextFactory<AppDbContext> dbContext, ILogger<StudentService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    protected async Task<StudentUser?> FindStudentById(Guid? id)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var student = await context
            .StudentUsers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student is null)
            _logger.LogError("--> Student(Id: {StudentId}) was not found", id);
    
        return student;
    }
}
