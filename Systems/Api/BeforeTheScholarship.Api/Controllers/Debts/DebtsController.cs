using AutoMapper;
using BeforeTheScholarship.Common.Security;
using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Services.DebtService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BeforeTheScholarship.Api.Controllers.Debts;

/// <summary>
/// Debts controller
/// </summary>
[Produces("application/json")]
[Route("api/v{version:apiVersion}/debts")]
[EnableCors(PolicyName = CorsSettings.DefaultOriginName)]
[ApiController]
//[Authorize]
[ApiVersion("1.0")]
public class DebtsController : ControllerBase
{
    private readonly IDebtService _debtService;
    private readonly ILogger<DebtsController> _logger;
    private readonly IMapper _mapper;

    /// <summary>
    /// Debts constructor that implements services
    /// </summary>
    public DebtsController(
        IDebtService debtService,
        ILogger<DebtsController> logger,
        IMapper mapper
        )
    {
        _debtService = debtService;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// HttpGet - Gettings debts from database
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(typeof(IEnumerable<DebtResponse>), 200)]
    //[Authorize(Policy = AppScopes.DebtsRead)]
    [HttpGet("")]
    public async Task<IEnumerable<DebtResponse>> GetDebts()
    {
        _logger.LogInformation("--> Trying to return all debts..");

        var debts = await _debtService.GetDebts();

        var response = debts.Select(_mapper.Map<DebtResponse>);

        return response;
    }
    
    /// <summary>
    /// Returns debts that owned by StudentUser with <paramref name="studentId"/>
    /// </summary>
    /// <param name="studentId">Unique student identifier</param>
    [ProducesResponseType(typeof(IEnumerable<DebtResponse>), 200)]
    //[Authorize(Policy = AppScopes.DebtsRead)]
    [HttpGet("{studentId}")]
    public async Task<IEnumerable<DebtResponse>?> GetDebts([FromRoute] Guid? studentId)
    {
        _logger.LogInformation("--> Debts belonging to a Student (Id: {StudentId} are returned..", studentId);

        var debts = await _debtService.GetDebts(studentId);

        var response = debts?.Select(_mapper.Map<DebtResponse>);

        return response;
    }

    /// <summary>
    /// Returns overdue debts that belong to student with <paramref name="studentId"/>
    /// </summary>
    /// <param name="studentId">Identifier of the student whose debts must be repaid</param>
    [ProducesResponseType(typeof(IEnumerable<DebtResponse>), 200)]
    //[Authorize(Policy = AppScopes.DebtsRead)]
    [HttpGet("overdue")]
    public async Task<IEnumerable<DebtResponse>?> GetOverdueDebts([FromQuery] Guid? studentId)
    {
        _logger.LogInformation("--> Trying to pay back Student (Id: {StudentId}) debts that have been overdue", studentId);

        var debts = await _debtService.GetOverdueDebts(studentId);

        var response = debts?.ToList().Select(_mapper.Map<DebtResponse>);

        return response;
    }

    /// <summary>
    /// Returns debts that need to be urgently repaid
    /// </summary>
    /// <param name="studentId">Identifier of the student whose debts must be repaid</param>
    [ProducesResponseType(typeof(IEnumerable<DebtResponse>), 200)]
    //[Authorize(Policy = AppScopes.DebtsRead)]
    [HttpGet("urgently-repay")]
    public async Task<IEnumerable<DebtResponse>> GetUrgentlyRepaidDebts([FromQuery] Guid? studentId)
    {
        _logger.LogInformation("--> Trying to pay back Student (Id: {StudentId}) debts that need to be repaid urgently", studentId);

        var debts = await _debtService.GetUrgentlyRepaidDebts(studentId);

        var response = debts.ToList().Select(_mapper.Map<DebtResponse>);

        return response;
    }

    /// <summary>
    /// HttpPost - Adds new debt to database
    /// </summary>
    /// <param name="request"></param>
    [ProducesResponseType(typeof(CreateDebtResponse), 200)]
    //[Authorize(Policy = AppScopes.DebtsWrite)]
    [HttpPost("")]
    public async Task<CreateDebtResponse> CreateDebt([FromBody] CreateDebtRequest request)
    {
        _logger.LogInformation("--> Trying to create Debt (StudentId: {StudentId})", request.StudentId);

        var model = _mapper.Map<CreateDebtModel>(request);

        var response = await _debtService.CreateDebt(model);

        return response;
    }

    /// <summary>
    /// HttpPut - Updates existing debt with <paramref name="id"/> in database
    /// </summary>
    /// <param name="id">Unique debt identifier</param>
    /// <param name="request">Request body</param>
    [ProducesResponseType(typeof(UpdateDebtResponse), 400)]
    [ProducesResponseType(typeof(UpdateDebtResponse), 200)]
    //[Authorize(Policy = AppScopes.DebtsWrite)]
    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateDebtResponse>> UpdateDebt([FromRoute] Guid? id, [FromBody] UpdateDebtRequest request)
    {
        _logger.LogInformation("--> Trying to update Debt (Id: {DebtId})", id);

        var model = _mapper.Map<UpdateDebtModel>(request);
        
        var response = await _debtService.UpdateDebt(id, model);

        if (response is null)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// HttpDelete - Deletes existing debt in database
    /// </summary>
    /// <param name="id">Unique debt identifier</param>
    [ProducesResponseType(typeof(DeleteDebtResponse), 400)]
    [ProducesResponseType(typeof(DeleteDebtResponse), 200)]
    //[Authorize(Policy = AppScopes.DebtsWrite)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<DeleteDebtResponse>> DeleteDebt([FromRoute] Guid? id)
    {
        _logger.LogInformation("--> Trying to remove Debt (Id: {DebtId})", id);

        var response = await _debtService.DeleteDebt(id);

        if (response == null)
            return BadRequest(response);

        return Ok(response);
    }
}
