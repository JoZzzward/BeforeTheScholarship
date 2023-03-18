using AutoMapper;
using BeforeTheScholarship.Services.UserAccountService;
using BeforeTheScholarship.Services.UserAccountService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BeforeTheScholarship.Api.Controllers.Accounts;

/// <summary>
/// Controller to manage account
/// </summary>
/// <response code="401">Unauthorized</response>
[Produces("application/json")]
[Route("api/v{version:apiVersion}/account")]
[ApiController]
[EnableCors(PolicyName = CorsSettings.DefaultOriginName)]
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
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<RegisterUserAccountResponse> Register([FromQuery] RegisterUserAccountRequest request)
    {
        _logger.LogInformation("--> User(UserName: {UserUserName}) trying to register.", request.UserName);

        var user = await _userAccountService.RegisterUser(_mapper.Map<RegisterUserAccountModel>(request));

        var response = _mapper.Map<RegisterUserAccountResponse>(user);

        return response;
    }

    /// <summary>
    /// Performs login for the user with the specified email
    /// </summary>
    /// <param name="request">Contains user email and password</param>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<LoginUserAccountResponse> Login([FromQuery] LoginUserAccountRequest request)
    {
        _logger.LogInformation("--> User(Email: {UserEmail}) trying to sign in.", request.Email);

        var user = await _userAccountService.LoginUser(_mapper.Map<LoginUserAccountModel>(request));

        var response = _mapper.Map<LoginUserAccountResponse>(user);

        return response;
    }

    /// <summary>
    /// Confirm email with token that was given on account registration and sended to user email
    /// </summary>
    /// <param name="request">Contains email and token for confirmation</param>
    [HttpPost("confirmemail")]
    public async Task<ConfirmationEmailResponse> ConfirmEmail([FromBody] ConfirmationEmailRequest request)
    {
        _logger.LogInformation("--> User(Email: {UserEmail}) trying to confirm email.", request.Email);
        
        var confirmEmailModel = _mapper.Map<ConfirmationEmailModel>(request);

        var response = await _userAccountService.ConfirmEmail(confirmEmailModel);

        return response;
    }

    /// <summary>
    /// Sending password recovery mail on user email that specified in <paramref name="request"/>
    /// </summary>
    /// <param name="request">Contains user email to send the mail to</param>
    [HttpPost("recover-password-mail")]
    public async Task<PasswordRecoveryResponse> SendRecoverPassword([FromBody] PasswordRecoveryMailRequest request)
    {
        _logger.LogInformation("--> User(Email: {UserEmail}) trying to send password recover message on his email.", request.Email);

        var model = _mapper.Map<SendPasswordRecoveryModel>(request);

        var response = await _userAccountService.SendRecoveryPasswordEmail(model);

        return response;
    }

    /// <summary>
    /// Recover password on new password from request to user with given email.
    /// </summary>
    /// <param name="request">Contains email on what password will be recovered, token from mail and new password</param>
    [HttpPost("recover-password")]
    public async Task<PasswordRecoveryResponse> RecoverPassword([FromBody] PasswordRecoveryRequest request)
    {
        _logger.LogInformation("--> User(Email: {UserEmail}) trying to recover his password.", request.Email);

        var model = _mapper.Map<PasswordRecoveryModel>(request);

        var response = await _userAccountService.RecoverPassword(model);

        return response;
    }

    /// <summary>
    /// Changes user with given email old password on new password.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("change-password")]
    public async Task<ChangePasswordResponse> ChangePassword([FromQuery] ChangePasswordRequest request)
    {
        _logger.LogInformation("--> User(Email: {UserEmail}) trying to change his password.", request.Email);
        var model = _mapper.Map<ChangePasswordModel>(request);

        var response = await _userAccountService.ChangePassword(model);

        return response;
    }
}
