using AutoMapper;
using BeforeTheScholarship.Services.StudentService;
using BeforeTheScholarship.Services.StudentService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BeforeTheScholarship.Api.Controllers.Students;

/// <summary>
/// Students controller
/// </summary>
[Produces("application/json")]
[Route("api/v{version:apiVersion}/students")]
[EnableCors(PolicyName = CorsSettings.DefaultOriginName)]
[ApiController]
[Authorize]
[ApiVersion("1.0")]
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
    /// HttpGet - Returns students from database
    /// </summary>
    [ProducesResponseType(typeof(IEnumerable<StudentResponse>), 200)]
    [AllowAnonymous]
    [HttpGet("")]
    public async Task<IEnumerable<StudentResponse>> GetStudents()
    {
        _logger.LogInformation("--> Trying to return all students..");

        var response = await _studentService.GetStudents();

        return response;
    }

    /// <summary>
    /// HttpGet - Returns <see cref="StudentResponse"/> with same <paramref name="id"/>
    /// </summary>
    /// <param name="id">Unique student identifier</param>
    [ProducesResponseType(typeof(StudentResponse), 200)]
    [HttpGet("{id}")]
    public async Task<StudentResponse> GetStudentById([FromRoute] Guid id)
    {
        _logger.LogInformation("--> Student(Id: {StudentId}) trying to return..", id);

        var response = await _studentService.GetStudentById(id);
            
        return response;
    }

    /// <summary>
    /// HttpPut - Updates existed StudentUser in database
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateStudentResponse>> UpdateStudent([FromRoute] Guid id, [FromBody] UpdateStudentRequest request)
    {
        _logger.LogInformation("--> Trying to update student(Id: {StudentId})", id);

        var model = _mapper.Map<UpdateStudentModel>(request);

        var response = await _studentService.UpdateStudent(id, model);

        if (response is null)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// HttpDelete - Deletes existed StudentUser in database
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<DeleteStudentResponse>> DeleteStudent([FromRoute] Guid? id)
    {
        _logger.LogInformation("--> Trying to remove student(Id: {StudentId})", id);

        var response = await _studentService.DeleteStudent(id);

        if (response is null)
            return BadRequest(response);

        return Ok(response);
    }
}
