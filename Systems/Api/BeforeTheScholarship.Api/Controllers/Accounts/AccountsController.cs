﻿using AutoMapper;
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
    [HttpPost("register")]
    public async Task<RegisterUserAccountResponse> Register([FromBody] RegisterUserAccountRequest request)
    {
        _logger.LogInformation("--> User (Email: {UserEmail}) trying to register.", request.Email);

        var model = _mapper.Map<RegisterUserAccountModel>(request);

        var user = await _userAccountService.RegisterUser(model);

        var response = _mapper.Map<RegisterUserAccountResponse>(user);

        return response;
    }

    /// <summary>
    /// Performs login for the user with the specified email
    /// </summary>
    /// <param name="request">Contains user email and password</param>
    [ProducesResponseType(typeof(LoginUserAccountResponse), 200)]
    [HttpPost("login")]
    public async Task<LoginUserAccountResponse> Login([FromBody] LoginUserAccountRequest request)
    {
        _logger.LogInformation("--> User (Email: {UserEmail}) trying to sign in.", request.Email);

        var model = _mapper.Map<LoginUserAccountModel>(request);

        var user = await _userAccountService.LoginUser(model);

        var response = _mapper.Map<LoginUserAccountResponse>(user);

        return response;
    }

    /// <summary>
    /// Confirm email with token that was given on account registration and send to user email
    /// </summary>
    /// <param name="request">Contains email and token for confirmation</param>
    [ProducesResponseType(typeof(ConfirmationEmailResponse), 200)]
    [HttpPost("confirm-email")]
    public async Task<ConfirmationEmailResponse> ConfirmEmail([FromBody] ConfirmationEmailRequest request)
    {
        _logger.LogInformation("--> User (Email: {UserEmail}) trying to confirm email.", request.Email);

        var confirmEmailModel = _mapper.Map<ConfirmationEmailModel>(request);

        var response = await _userAccountService.ConfirmEmail(confirmEmailModel);

        return response;
    }

    /// <summary>
    /// Sending password recovery mail on user email that specified in <paramref name="request" />
    /// </summary>
    /// <param name="request">Contains user email to send the mail to</param>
    [ProducesResponseType(typeof(PasswordRecoveryResponse), 200)]
    [HttpPost("recover-password-mail")]
    public async Task<PasswordRecoveryResponse> SendRecoverPassword([FromBody] PasswordRecoveryMailRequest request)
    {
        _logger.LogInformation("--> User (Email: {UserEmail}) trying to send password recover message on his email.",
            request.Email);

        var model = _mapper.Map<PasswordRecoveryMailModel>(request);

        var response = await _userAccountService.SendRecoveryPasswordEmail(model);

        return response;
    }

    /// <summary>
    /// Recover password on new password from request to user with given email.
    /// </summary>
    /// <param name="request">Contains email on what password will be recovered, token from mail and new password</param>
    [ProducesResponseType(typeof(PasswordRecoveryResponse), 200)]
    [HttpPost("recover-password")]
    public async Task<PasswordRecoveryResponse> RecoverPassword([FromBody] PasswordRecoveryRequest request)
    {
        _logger.LogInformation("--> User (Email: {UserEmail}) trying to recover his password.", request.Email);

        var model = _mapper.Map<PasswordRecoveryModel>(request);

        var response = await _userAccountService.RecoverPassword(model);

        return response;
    }

    /// <summary>
    /// Changes user with given email old password on new password.
    /// </summary>
    /// <param name="request">Contains user credentials for password changing</param>
    [ProducesResponseType(typeof(ChangePasswordResponse), 200)]
    [Authorize]
    [HttpPost("change-password")]
    public async Task<ChangePasswordResponse> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        _logger.LogInformation("--> User (Email: {UserEmail}) trying to change his password.", request.Email);
        var model = _mapper.Map<ChangePasswordModel>(request);

        var response = await _userAccountService.ChangePassword(model);

        return response;
    }
}