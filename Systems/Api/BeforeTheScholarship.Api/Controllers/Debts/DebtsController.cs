using AutoMapper;
using BeforeTheScholarship.Common.Security;
using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Services.DebtService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BeforeTheScholarship.Api.Controllers.Debts;

/// <summary>
/// Debts ApiController
/// </summary>
[Route("api/v{version:apiVersion}/debts")]
[Produces("application/json")]
[Authorize]
[ApiController]
[EnableCors(PolicyName = CorsSettings.DefaultOriginName)]
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
    [Authorize(Policy = AppScopes.DebtsRead)]
    [HttpGet("")]
    public async Task<IEnumerable<DebtResponse>> GetDebts()
    {
        _logger.LogInformation("--> Trying to return all debts..");

        var debts = await _debtService.GetDebts();

        var response = debts.Select(x => _mapper.Map<DebtResponse>(x));

        return response;
    }
    
    /// <summary>
    /// HttpGet - Returns debts that owned by StudentUser with <paramref name="studentId"/>
    /// </summary>
    /// <param name="studentId">Unique student identifier</param>
    [ProducesResponseType(typeof(IEnumerable<DebtResponse>), 200)]
    [Authorize(Policy = AppScopes.DebtsRead)]
    [HttpGet("{studentId}")]
    public async Task<IEnumerable<DebtResponse>> GetDebts([FromRoute] Guid? studentId)
    {
        _logger.LogInformation("--> Debts belonging to a student(Id: {StudentId} are returned..", studentId);

        var debts = await _debtService.GetDebts(studentId);

        var response = debts.Select(x => _mapper.Map<DebtResponse>(x));

        _logger.LogInformation("--> Debts belong to a student(Id: {StudentId} was returned successfully.", studentId);

        return response;
    }

    /// <summary>
    /// HttpPost - Adds new debt to database
    /// </summary>
    /// <param name="request"></param>
    [Authorize(Policy = AppScopes.DebtsWrite)]
    [HttpPost("")]
    public async Task<CreateDebtResponse> CreateDebt([FromBody] AddDebtRequest request)
    {
        _logger.LogInformation("--> Trying to create debt(StudentId: {StudentId})..", request.StudentId);

        var model = _mapper.Map<CreateDebtModel>(request);

        var response = await _debtService.CreateDebt(model);

        return response;
    }

    /// <summary>
    /// HttpPut - Updates existing debt with <paramref name="id"/> in database
    /// </summary>
    /// <param name="id">Unique debt identifier</param>
    /// <param name="request">Request body</param>
    [Authorize(Policy = AppScopes.DebtsWrite)]
    [HttpPut("{id}")]
    public async Task<UpdateDebtResponse> UpdateDebt([FromRoute] int? id, [FromBody] UpdateDebtRequest request)
    {
        _logger.LogInformation("--> Trying to update debt(Id: {DebtId})..", id);

        var model = _mapper.Map<UpdateDebtModel>(request);
        
        var response = await _debtService.UpdateDebt(id, model);

        return response;
    }

    /// <summary>
    /// HttpDelete - Deletes existing debt in database
    /// </summary>
    /// <param name="id">Unique debt identifier</param>
    [Authorize(Policy = AppScopes.DebtsWrite)]
    [HttpDelete("{id}")]
    public async Task<DeleteDebtResponse> DeleteDebt([FromRoute] int? id)
    {
        _logger.LogInformation("--> Trying to remove debt(Id: {DebtId})..", id);

        var response = await _debtService.DeleteDebt(id);

        return response;
    }

    /// <summary>
    /// Returns debts that need to be urgently repaid
    /// </summary>
    /// <param name="studentId">Identifier of the student whose debts must be repaid</param>
    /// <param name="overdue">Whether the deadline for debt is overdue</param>
    [Authorize(Policy = AppScopes.DebtsRead)]
    [HttpGet("urgently-repay")]
    public async Task<IEnumerable<DebtResponse>> GetUrgentlyRepaidDebts([FromQuery] Guid studentId, [FromQuery] bool overdue)
    {
        _logger.LogInformation("--> Attempting to pay back student(Id: {StudentId}) debts that need to be repaid urgently or that have been overdue..", studentId);

        var debts = await _debtService.GetUrgentlyRepaidDebts(studentId, overdue);

        var response = debts.ToList().Select(x => _mapper.Map<DebtResponse>(x));

        return response;
    }
}
