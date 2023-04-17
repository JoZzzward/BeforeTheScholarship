using AutoMapper;
using BeforeTheScholarship.Api.Controllers.Accounts.Models;
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
[Route("api/v{version:apiVersion}/accounts")]
[EnableCors(PolicyName = CorsSettings.DefaultOriginName)]
[ApiController]
[ApiVersion("1.0")]
public class AccountsController : ControllerBase
{
    private readonly ILogger<AccountsController> _logger;
    private readonly IMapper _mapper;
    private readonly IUserAccountService _userAccountService;

    public AccountsController(
        IUserAccountService userAccountService,
        IMapper mapper,
        ILogger<AccountsController> logger)
    {
        _userAccountService = userAccountService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Creates new user account and send email
    /// </summary>
    [ProducesResponseType(typeof(RegisterUserAccountResponse), 200)]
    [ProducesResponseType(typeof(RegisterUserAccountResponse), 400)]
    [HttpPost("register")]
    public async Task<ActionResult<RegisterUserAccountResponse>> Register([FromBody] RegisterUserAccountRequest request)
    {
        _logger.LogInformation("--> User (Email: {UserEmail}) trying to register.", request.Email);

        var model = _mapper.Map<RegisterUserAccountModel>(request);

        var user = await _userAccountService.RegisterUser(model);

        var response = _mapper.Map<RegisterUserAccountResponse>(user);

        if (response == null)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Performs login for the user with the specified email
    /// </summary>
    /// <param name="request">Contains user email and password</param>
    [ProducesResponseType(typeof(LoginUserAccountResponse), 200)]
    [ProducesResponseType(typeof(LoginUserAccountResponse), 400)]
    [HttpPost("login")]
    public async Task<ActionResult<LoginUserAccountResponse>> Login([FromBody] LoginUserAccountRequest request)
    {
        _logger.LogInformation("--> User (Email: {UserEmail}) trying to sign in.", request.Email);

        var model = _mapper.Map<LoginUserAccountModel>(request);

        var user = await _userAccountService.LoginUser(model);

        var response = _mapper.Map<LoginUserAccountResponse>(user);

        if (response == null)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Sending a message has been sent with which the user can confirm his mail
    /// </summary>
    /// <param name="request">Contains email and token for confirmation</param>
    [ProducesResponseType(typeof(ConfirmationEmailResponse), 200)]
    [ProducesResponseType(typeof(ConfirmationEmailResponse), 400)]
    [HttpPost("send-confirm-email")]
    public async Task<ActionResult<SendConfirmationEmailResponse>> SendConfirmEmail([FromBody] SendConfirmationEmailRequest request)
    {
        _logger.LogInformation("--> User (Email: {UserEmail}) trying to send confirmation email message.", request.Email);

        var model = _mapper.Map<SendConfirmationEmailModel>(request);

        var response = await _userAccountService.SendConfirmEmail(model);

        if (response == null)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Confirm email with token that was given on account registration and send to user email
    /// </summary>
    /// <param name="request">Contains email and token for confirmation</param>
    [ProducesResponseType(typeof(ConfirmationEmailResponse), 200)]
    [ProducesResponseType(typeof(ConfirmationEmailResponse), 400)]
    [HttpPost("confirm-email")]
    public async Task<ActionResult<ConfirmationEmailResponse>> ConfirmEmail([FromBody] ConfirmationEmailRequest request)
    {
        _logger.LogInformation("--> User (Email: {UserEmail}) trying to confirm email.", request.Email);

        var model = _mapper.Map<ConfirmationEmailModel>(request);

        var response = await _userAccountService.ConfirmEmail(model);

        if (response == null)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Sending password recovery mail on user email that specified in <paramref name="request" />
    /// </summary>
    /// <param name="request">Contains user email to send the mail to</param>
    [ProducesResponseType(typeof(PasswordRecoveryResponse), 200)]
    [ProducesResponseType(typeof(PasswordRecoveryResponse), 400)]
    [HttpPost("send-recover-password")]
    public async Task<ActionResult<PasswordRecoveryResponse>> SendRecoverPassword([FromBody] SendPasswordRecoveryRequest request)
    {
        _logger.LogInformation("--> User (Email: {UserEmail}) trying to send password recover message on his email.",
            request.Email);

        var model = _mapper.Map<SendPasswordRecoveryModel>(request);

        var response = await _userAccountService.SendRecoveryPasswordEmail(model);

        if (response == null)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Recover password on new password from request to user with given email.
    /// </summary>
    /// <param name="request">Contains email on what password will be recovered, token from mail and new password</param>
    [ProducesResponseType(typeof(PasswordRecoveryResponse), 200)]
    [ProducesResponseType(typeof(PasswordRecoveryResponse), 400)]
    [HttpPost("recover-password")]
    public async Task<ActionResult<PasswordRecoveryResponse>> RecoverPassword([FromBody] PasswordRecoveryRequest request)
    {
        _logger.LogInformation("--> User (Email: {UserEmail}) trying to recover his password.", request.Email);

        var model = _mapper.Map<PasswordRecoveryModel>(request);

        var response = await _userAccountService.RecoverPassword(model);

        if (response == null)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Changes user with given email old password on new password.
    /// </summary>
    /// <param name="request">Contains user credentials for password changing</param>
    [ProducesResponseType(typeof(ChangePasswordResponse), 200)]
    [ProducesResponseType(typeof(ChangePasswordResponse), 400)]
    [Authorize]
    [HttpPost("change-password")]
    public async Task<ActionResult<ChangePasswordResponse>> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        _logger.LogInformation("--> User (Email: {UserEmail}) trying to change his password.", request.Email);
        var model = _mapper.Map<ChangePasswordModel>(request);

        var response = await _userAccountService.ChangePassword(model);

        if (response == null)
            return BadRequest(response);

        return Ok(response);
    }
}

