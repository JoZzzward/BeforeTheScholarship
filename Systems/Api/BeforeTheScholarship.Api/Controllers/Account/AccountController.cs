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
        _logger.LogInformation("--> User(UserName: {UserUserName}) trying to register.", request.UserName);

        var user = await _userAccountService.RegisterUser(_mapper.Map<RegisterUserAccountModel>(request));

        var response = _mapper.Map<UserAccountResponse>(user);

        _logger.LogInformation("--> User(UserName: {UserUserName}) was succesfully registered.", user.UserName);

        return response;
    }

    [HttpPost("login")]
    public async Task<UserAccountResponse> Login([FromQuery] LoginUserAccountRequest request)
    {
        _logger.LogInformation("--> User(Email: {UserEmail}) trying to sign in.", request.Email);

        var user = await _userAccountService.LoginUser(_mapper.Map<LoginUserAccountModel>(request));

        var response = _mapper.Map<UserAccountResponse>(user);

        _logger.LogInformation("--> User(Email: {UserEmail}) was succesfully sign in.", user.UserName);

        return response;
    }

    /// <summary>
    /// Confirm email with token that was given on account registration and sended to user email
    /// </summary>
    /// <param name="request">Contains email and token for confirmation</param>
    [HttpPost("confirmemail")]
    public async Task ConfirmEmail([FromBody] ConfirmationEmailRequest request)
    {
        _logger.LogInformation("--> User(Email: {UserEmail}) trying to confirm email.", request.Email);
        
        var confirmEmailModel = _mapper.Map<ConfirmationEmailModel>(request);

        await _userAccountService.ConfirmEmail(confirmEmailModel);

        _logger.LogInformation("--> User(Email: {UserEmail}) successfully confirm his email.", request.Email);
    }

    /// <summary>
    /// Sending password recovery mail on user email that specified in <paramref name="request"/>
    /// </summary>
    /// <param name="request">Contains user email to send the mail to</param>
    [HttpPost("recover-password-mail")]
    public async Task SendRecoverPassword([FromBody] PasswordRecoveryMailRequest request)
    {
        _logger.LogInformation("--> User(Email: {UserEmail}) trying to send password recover message on his email.", request.Email);

        var model = _mapper.Map<SendPasswordRecoveryModel>(request);

        var response = await _userAccountService.SendRecoveryPasswordEmail(model);

        _logger.LogInformation("--> Password message was successfully sended to User(Email: {UserEmail})", response.Email);
    }

    /// <summary>
    /// Recover password on new password from request to user with given email.
    /// </summary>
    /// <param name="request">Contains email on what password will be recovered, token from mail and new password</param>
    [HttpPost("recover-password")]
    public async Task RecoverPassword([FromBody] PasswordRecoveryRequest request)
    {
        _logger.LogInformation("--> User(Email: {UserEmail}) trying to recover his password.", request.Email);

        var model = _mapper.Map<PasswordRecoveryModel>(request);

        var response = await _userAccountService.RecoverPassword(model);

        _logger.LogInformation("--> Password of User(Email: {UserEmail}) was successfully recovered.", response.Email);
    }

    /// <summary>
    /// Changes user with given email old password on new password.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("change-password")]
    public async Task ChangePassword([FromQuery] ChangePasswordRequest request)
    {
        _logger.LogInformation("User(Email: {UserEmail}) trying to change his password.", request.Email);
        var model = _mapper.Map<ChangePasswordModel>(request);

        var response = await _userAccountService.ChangePassword(model);

        _logger.LogInformation("Password of User(Email: {UserEmail}) was successfully changed.", response.Email);
    }
}
