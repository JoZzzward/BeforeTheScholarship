using Microsoft.AspNetCore.Mvc;

namespace BeforeTheScholarship.Api.Controllers.Debts;

/// <summary>
/// Debts controller
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
    /// HttpGet method that returns the list of debts 
    /// </summary>
    /// <returns> IEnumerable<see cref="{DebtModel}"/></returns>
    [HttpGet]
    public async Task<IEnumerable<DebtModel>> GetDebts()
    {
        var debts = new List<DebtModel>()
        {
            new DebtModel { Id = 1, Cost = 100 },
            new DebtModel { Id = 2, Cost = 200 },
            new DebtModel { Id = 3, Cost = 300 }
        };

        _logger.LogInformation("Debts was returned successfully!");

        return debts;
    }

    /// <summary>
    /// Sends an Error log to file "errorlog.txt"
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task ThrowError()
    {
        _logger.LogError("Error");
    }
}
