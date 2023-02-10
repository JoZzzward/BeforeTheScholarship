using Microsoft.AspNetCore.Mvc;

namespace BeforeTheScholarship.Api.Controllers.Debts;

/// <summary>
/// Debts ApiController with implemented CRUD.
/// </summary>
[ApiController]
[Route("api/debts")]
public class DebtsController : ControllerBase
{
    private readonly ILogger<DebtsController> _logger;

    public DebtsController(ILogger<DebtsController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Async HttpGet method that returns the list of debts 
    /// </summary>
    [HttpGet]
    public async Task<IEnumerable<DebtModel>> GetDebts()
    {
        var debts = new List<DebtModel>()
        {
            new DebtModel { Id = 1, Cost = 100 },
            new DebtModel { Id = 2, Cost = 200 },
            new DebtModel { Id = 3, Cost = 300 }
        };
        //todo: 

        _logger.LogInformation("Debts was returned successfully!");

        return debts;
    }
}
