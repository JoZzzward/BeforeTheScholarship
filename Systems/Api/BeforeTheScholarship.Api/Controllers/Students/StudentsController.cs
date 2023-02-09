using BeforeTheScholarship.Api.Controllers.Debts;
using Microsoft.AspNetCore.Mvc;

namespace BeforeTheScholarship.Api.Controllers.Students;

/// <summary>
/// Students controller
/// </summary>
[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly ILogger<DebtsController> _logger;

    public StudentsController(ILogger<DebtsController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// HttpGet method that returns the list of students 
    /// </summary>
    /// <returns> IEnumerable<see cref="{StudentModel}"/></returns>
    [ProducesResponseType(typeof(IEnumerable<StudentModel>), 200)]
    [HttpGet]
    public async Task<IEnumerable<StudentModel>> GetStudents()
    {
        var students = new List<StudentModel>()
        {
            new StudentModel { Id = 1, Name = "John" },
            new StudentModel { Id = 2, Name = "Clara" },
            new StudentModel { Id = 3, Name = "Lucy" }
        };

        _logger.LogInformation("Students was returned successfully!");

        return students;
    }
}
