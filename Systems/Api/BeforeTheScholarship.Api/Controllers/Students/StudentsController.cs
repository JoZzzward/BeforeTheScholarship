using AutoMapper;
using BeforeTheScholarship.Services.CacheService;
using BeforeTheScholarship.Services.StudentService;
using BeforeTheScholarship.Services.StudentService.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BeforeTheScholarship.Api.Controllers.Students;

/// <summary>
/// Students controller
/// </summary>
[Produces("application/json")]
[ApiController]
[EnableCors(PolicyName = CorsSettings.DefaultOriginName)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly ILogger<StudentsController> _logger;
    private readonly IMapper _mapper;

    /// <summary>
    /// Students constructor that implements services
    /// </summary>
    public StudentsController(
        IStudentService studentService,
        ILogger<StudentsController> logger,
        IMapper mapper)
    {
        _studentService = studentService;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// HttpGet - Gettings students from database
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(typeof(IEnumerable<StudentModel>), 200)]
    [HttpGet("")]
    public async Task<IEnumerable<StudentResponse>> GetStudents()
    {
        _logger.LogInformation("--> Trying to return all students..");

        var students = await _studentService.GetStudents();
        var response = students.Select(_mapper.Map<StudentResponse>);

        return response;
    }

    /// <summary>
    /// HttpGet - Returns <see cref="StudentResponse"/> with same <paramref name="id"/>
    /// </summary>
    /// <param name="id">Unique student identifier</param>
    [HttpGet("{id}")]
    public async Task<StudentResponse> GetStudentById([FromRoute] Guid id)
    {
        _logger.LogInformation("--> Student(Id: {StudentId}) trying to return..", id);

        var student = await _studentService.GetStudentById(id);
            
        var response = _mapper.Map<StudentResponse>(student);

        return response;
    }

    /// <summary>
    /// HttpPut - Updates existed StudentUser in database
    /// </summary>
    [HttpPut("{id}")]
    public async Task<UpdateStudentResponse> UpdateStudent([FromRoute]Guid id, [FromBody] UpdateStudentRequest request)
    {
        _logger.LogInformation("--> Trying to update student(Id: {StudentId})", id);

        var model = _mapper.Map<UpdateStudentModel>(request);

        var response = await _studentService.UpdateStudent(id, model);

        return response;
    }

    /// <summary>
    /// HttpDelete - Deletes existed StudentUser in database
    /// TODO: Delete this method instead of future AccountController
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<DeleteStudentResponse> DeleteStudent([FromRoute] Guid? id)
    {
        _logger.LogInformation("--> Trying to remove student(Id: {StudentId})", id);

        var response = await _studentService.DeleteStudent(id);

        return response;
    }
}
