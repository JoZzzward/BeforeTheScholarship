namespace BeforeTheScholarship.API.Controllers;

using AutoMapper;
using BeforeTheScholarship.Api.Controllers.Account.Models;
using BeforeTheScholarship.Services.UserAccount;
using BeforeTheScholarship.UserAccountService.Models;
using Microsoft.AspNetCore.Mvc;

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
    [HttpPost("register")]
    public async Task<UserAccountResponse> Register([FromQuery] RegisterUserAccountRequest request)
    {
        var user = await _userAccountService.RegisterUser(_mapper.Map<RegisterUserAccountModel>(request));

        var response = _mapper.Map<UserAccountResponse>(user);

        _logger.LogInformation($"--> User(name: {user.UserName}) was succesfully registered!");

        return response;
    }

    /// <summary>
    /// Confirm email with token that was given on account registration
    /// </summary>
    /// <param name="request">Contains email and token for confirmation</param>
    [HttpPost("confirmemail")]
    public async Task ConfirmEmail([FromBody] ConfirmationEmailRequest request)
    {
        var confirmEmailModel = _mapper.Map<ConfirmationEmailModel>(request);

        await _userAccountService.ConfirmEmail(confirmEmailModel);

        _logger.LogInformation($"--> User(email: {request.Email}) succesfully confirmed email!");
    }

    [HttpPost("recover-password-mail")]
    public async Task SendRecoverPassword([FromBody] PasswordRecoveryMailRequest request)
    {
        var model = _mapper.Map<SendPasswordRecoveryModel>(request);

        var response = await _userAccountService.SendRecoveryPasswordEmail(model);

        _logger.LogInformation($"Password of User(UserName: {response.UserName}) was successfully recovered!");
    }


    [HttpPost("recover-password")]
    public async Task RecoverPassword([FromBody] PasswordRecoveryRequest request)
    {
        var model = _mapper.Map<PasswordRecoveryModel>(request);

        var response = await _userAccountService.RecoverPassword(model);

        _logger.LogInformation($"Password of User(UserName: {response.UserName}) was successfully recovered!");
    }
}
