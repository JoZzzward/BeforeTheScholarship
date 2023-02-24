namespace BeforeTheScholarship.API.Controllers;

using AutoMapper;
using BeforeTheScholarship.Services.UserAccount;
using BeforeTheScholarship.API.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using BeforeTheScholarship.Api.Controllers.Accounts.Models;
using BeforeTheScholarship.Services.UserAccount.Models;

/// <summary>
/// Controller to manage account
/// </summary>
/// <response code="401">Unauthorized</response>
[Produces("application/json")]
[Route("api/v{version:apiVersion}/account")]
[ApiController]
[ApiVersion("1.0")]
public class AccountsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger<AccountsController> _logger;
    private readonly IUserAccountService _userAccountService;

    /// <summary>
    /// Accounts constructor that implements services
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="logger"></param>
    /// <param name="userAccountService"></param>
    public AccountsController(
        IMapper mapper, 
        ILogger<AccountsController> logger, 
        IUserAccountService userAccountService)
    {
        _mapper = mapper;
        _logger = logger;
        _userAccountService = userAccountService;
    }

    /// <summary>
    /// Creates new user account and send email 
    /// </summary>
    /// <param name="request"></param>
    [HttpPost("/register")]
    public async Task<UserAccountResponse> Register([FromBody] RegisterUserAccountRequest request)
    {
        var user = await _userAccountService.Create(_mapper.Map<RegisterUserAccountModel>(request));

        var response = _mapper.Map<UserAccountResponse>(user);

        _logger.LogInformation($"--> User(name: {user.FirstName}) was succesfully registered!");

        return response;
    }

    /// <summary>
    /// Confirm email with token that was given on account registration
    /// </summary>
    /// <param name="request">Contains email and token for confirmation</param>
    [HttpPost("confirmemail")]
    public async Task ConfirmEmail([FromBody] ConfirmationEmailRequest request)
    {
        var confirmEmailModel = _mapper.Map<ConfirmationEmail>(request);

        await _userAccountService.ConfirmEmail(confirmEmailModel);

        _logger.LogInformation($"--> User(email: {request.Email}) succesfully confirmed email!");
    }
}
