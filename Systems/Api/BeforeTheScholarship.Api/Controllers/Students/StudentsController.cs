using AutoMapper;
using BeforeTheScholarship.Api.Controllers.Debts;
using BeforeTheScholarship.StudentService;
using Microsoft.AspNetCore.Mvc;

namespace BeforeTheScholarship.Api.Controllers.Students;

/// <summary>
/// Students controller
/// </summary>
[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly ILogger<DebtsController> _logger;
    private readonly IMapper _mapper;

    /// <summary>
    /// Students constructor that implements services
    /// </summary>
    public StudentsController(
        IStudentService studentService,
        ILogger<DebtsController> logger,
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
        var data = students.Select(x => _mapper.Map<StudentResponse>(x));

        _logger.LogInformation("--> Students(Count: {StudentsCount}) was returned successfully!", data.Count());

        return data;
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
            
        var data = _mapper.Map<StudentResponse>(student);

        _logger.LogInformation("--> The student(Id: {StudentId}) was successfully returned!", id);

        return data;
    }

    /// <summary>
    /// HttpPost - Adds new StudentUser to database
    /// TODO: Delete this method instead of future AccountController
    /// </summary>
    /// <param name="request"></param>
    [HttpPost("")]
    public async Task<StudentResponse> CreateStudent([FromBody]AddStudentRequest request)
    {
        _logger.LogInformation("--> Trying to create student(Id: {StudentId})", request.UserName);

        var model = _mapper.Map<AddStudentModel>(request);
        var student = await _studentService.CreateStudent(model);
        var response = _mapper.Map<StudentResponse>(student);

        _logger.LogInformation("--> Student(Id: {StudentId}) was successfully created.", response.Id);

        return response;
    }

    /// <summary>
    /// HttpPut - Updates existed StudentUser in database
    /// TODO: Delete this method instead of future AccountController
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent([FromRoute]Guid id, [FromBody] UpdateStudentRequest request)
    {
        _logger.LogInformation("--> Trying to update student(Id: {StudentId})", id);

        var model = _mapper.Map<UpdateStudentModel>(request);
        await _studentService.UpdateStudent(id, model);

        _logger.LogInformation("--> Student(Id: {StudentId}) was successfully updated.", id);

        return Ok();
    }

    /// <summary>
    /// HttpDelete - Deletes existed StudentUser in database
    /// TODO: Delete this method instead of future AccountController
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent([FromRoute] Guid? id)
    {
        _logger.LogInformation("--> Trying to remove student(Id: {StudentId})", id);

        await _studentService.DeleteStudent(id);

        _logger.LogInformation("--> Student(Id: {StudentId}) was successfully removed.", id);

        return Ok();
    }
}
