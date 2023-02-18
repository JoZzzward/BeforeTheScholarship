using AutoMapper;
using BeforeTheScholarship.Api.Controllers.Debts.Models;
using BeforeTheScholarship.DebtService;
using Microsoft.AspNetCore.Mvc;

namespace BeforeTheScholarship.Api.Controllers.Debts;

/// <summary>
/// Debts ApiController
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/debts")]
public class DebtsController : ControllerBase
{
    private readonly IDebtService _debtService;
    private readonly ILogger<DebtsController> _logger;
    private readonly IMapper _mapper;

    public DebtsController(
        IDebtService debtService,
        ILogger<DebtsController> logger,
        IMapper mapper)
    {
        _debtService = debtService;
        _logger = logger;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(IEnumerable<DebtResponse>), 200)]
    [HttpGet("")]
    public async Task<IEnumerable<DebtResponse>> GetDebts()
    {
        var debts = await _debtService.GetDebts();
        var data = debts.Select(x => _mapper.Map<DebtResponse>(x));

        _logger.LogInformation("--> Debts was returned successfully!");

        return data;
    }

    [ProducesResponseType(typeof(IEnumerable<DebtResponse>), 200)]
    [HttpGet("{studentId}")]
    public async Task<IEnumerable<DebtResponse>> GetDebts([FromRoute] int? studentId)
    {
        var debts = await _debtService.GetDebts(studentId);
        var data = debts.Select(x => _mapper.Map<DebtResponse>(x));

        _logger.LogInformation("--> Debts was returned successfully!");

        return data;
    }

    [HttpPost("")]
    public async Task<DebtResponse> CreateDebt([FromBody] AddDebtRequest request)
    {
        var model = _mapper.Map<AddDebtModel>(request);
        var debts = await _debtService.CreateDebt(model);
        var response = _mapper.Map<DebtResponse>(debts);

        _logger.LogInformation("--> Debt was successfully created!");

        return response;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDebt([FromRoute] int? id, [FromBody] UpdateDebtsRequest request)
    {
        var model = _mapper.Map<UpdateDebtModel>(request);
        await _debtService.UpdateDebt(id, model);

        _logger.LogInformation("--> Debt was successfully updated!");

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDebt([FromRoute] int? id)
    {
        await _debtService.DeleteDebt(id);

        _logger.LogInformation("--> Debt was successfully removed!");

        return Ok();
    }

    [HttpGet("urgently-repay")]
    public async Task<IEnumerable<DebtResponse>> GetUrgentlyRepaidDebts([FromQuery] int studentId, [FromQuery] bool overdue)
    {
        var debts = await _debtService.GetUrgentlyRepaidDebts(studentId, overdue);

        var data = debts.ToList().Select(x => _mapper.Map<DebtResponse>(x));

        _logger.LogInformation("--> Debts that needed to be repaid urgently have been successfully returned!");

        return data;
    }
}
