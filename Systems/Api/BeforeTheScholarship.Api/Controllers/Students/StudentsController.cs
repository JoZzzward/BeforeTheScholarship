using AutoMapper;
using BeforeTheScholarship.Api.Controllers.Debts;
using BeforeTheScholarship.StudentService;
using Microsoft.AspNetCore.Mvc;

namespace BeforeTheScholarship.Api.Controllers.Students;

/// <summary>
/// Students controller
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly ILogger<DebtsController> _logger;
    private readonly IMapper _mapper;

    public StudentsController(
        IStudentService studentService,
        ILogger<DebtsController> logger,
        IMapper mapper)
    {
        _studentService = studentService;
        _logger = logger;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(IEnumerable<StudentModel>), 200)]
    [HttpGet]
    public async Task<IEnumerable<StudentResponse>> GetStudents()
    {
        var students = await _studentService.GetStudents();
        var data = students.Select(x => _mapper.Map<StudentResponse>(x));

        _logger.LogInformation("--> Students was returned successfully!");

        return data;
    }

    [HttpGet("{id}")]
    public async Task<StudentResponse> GetStudentById([FromRoute]int id)
    {
        var student = await _studentService.GetStudentById(id);
            
        var data = _mapper.Map<StudentResponse>(student);

        _logger.LogInformation($"--> The Student({id}) was returned successfully!");

        return data;
    }

    [HttpPost]
    public async Task<StudentResponse> CreateStudent([FromBody]AddStudentRequest request)
    {
        var model = _mapper.Map<AddStudentModel>(request);
        var student = await _studentService.CreateStudent(model);
        var response = _mapper.Map<StudentResponse>(student);

        return response;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent([FromRoute]int id, [FromBody] UpdateStudentRequest request)
    {
        var model = _mapper.Map<UpdateStudentModel>(request);
        await _studentService.UpdateStudent(id, model);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent([FromRoute] int? id)
    {
        await _studentService.DeleteStudent(id);

        return Ok();
    }
}
