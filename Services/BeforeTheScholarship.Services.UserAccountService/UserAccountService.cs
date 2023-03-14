namespace BeforeTheScholarship.Services.UserAccount;

using AutoMapper;
using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Common.Validation;
using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Services.EmailSender;
using BeforeTheScholarship.UserAccountService.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.IO;

public class UserAccountService : IUserAccountService
{
    private readonly IMapper _mapper;
    private readonly UserManager<StudentUser> _userManager;
    private readonly SignInManager<StudentUser> _signInManager;
    private readonly IEmailSender _emailSender;
    #region Model Validators
    private readonly IModelValidator<RegisterUserAccountModel> _registerModelValidator;
    private readonly IModelValidator<LoginUserAccountModel> _loginModelValidator;
    private readonly IModelValidator<ConfirmationEmailModel> _confirmationEmailModelValidator;
    private readonly IModelValidator<SendPasswordRecoveryModel> _sendPasswordRecoveryModelValidator;
    private readonly IModelValidator<PasswordRecoveryModel> _passwordRecoveryModelValidator;
    private readonly IModelValidator<ChangePasswordModel> _changePasswordModelValidator;
    #endregion

    public UserAccountService(
        IMapper mapper,
        UserManager<StudentUser> userManager,
        SignInManager<StudentUser> signInManager,
        IEmailSender emailSender,
    #region Model Validators
        IModelValidator<RegisterUserAccountModel> registerModelValidator,
        IModelValidator<LoginUserAccountModel> loginModelValidator,
        IModelValidator<ConfirmationEmailModel> confirmationEmailModelValidator,
        IModelValidator<SendPasswordRecoveryModel> sendPasswordRecoveryModelValidator,
        IModelValidator<PasswordRecoveryModel> passwordRecoveryModelValidator,
        IModelValidator<ChangePasswordModel> changePasswordModelValidator
    #endregion
        )
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _emailSender = emailSender;
        #region Model Validators

        _registerModelValidator = registerModelValidator;
        _loginModelValidator = loginModelValidator;
        _confirmationEmailModelValidator = confirmationEmailModelValidator;
        _sendPasswordRecoveryModelValidator = sendPasswordRecoveryModelValidator;
        _passwordRecoveryModelValidator = passwordRecoveryModelValidator;
        _changePasswordModelValidator = changePasswordModelValidator;

        #endregion
    }

    public async Task<UserAccountModel> RegisterUser(RegisterUserAccountModel model)
    {
        _registerModelValidator.CheckValidation(model);

        model.Email = model.Email.Replace(" ", "");

        var user = await _userManager.FindByEmailAsync(model.Email);
        
        if (user != null)
            throw new Exception($"User account with email {model.Email} already exist.");

        user = new StudentUser()
        {
            UserName = model.UserName ?? "User", 
            FirstName = "",
            LastName = "",
            Email = model.Email,
            EmailConfirmed = false,
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
        };
         
        var result = await _userManager.CreateAsync(user, model.Password);
        
        if (!result.Succeeded)
            throw new Exception($"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");

        if (!string.IsNullOrEmpty(model.Email))
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var content = PathReader.ReadContent(
                                Path.Combine(Directory.GetCurrentDirectory(), "\\EmailPages\\emailConfirmation.html"),
                                "/app/emailpages/emailConfirmation.html");

            content = content.Replace("QUERYEMAIL", user.Email)
                             .Replace("QUERYTOKEN", token)
                             .Replace("DATENOW", DateTime.Now.ToLocalTime().ToShortDateString().ToString())
                             ;

            // Sending email confirmation mail for current user
            _emailSender?.SendEmail(new EmailModel()
            {
                EmailTo = user.Email,
                Subject = $"Hello, dear {char.ToUpper(user.UserName[0]) + user.UserName.Substring(1)}!",
                Message = content
            });
        }

        return _mapper.Map<UserAccountModel>(user);
    }

    public async Task<UserAccountModel> LoginUser(LoginUserAccountModel model)
    {
        _loginModelValidator.CheckValidation(model);

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null)
            throw new ArgumentNullException($"User with email {model.Email} not found.");

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

        if (!result.Succeeded)
            throw new Exception($"Invalid email or password.");

        var data = _mapper.Map<UserAccountModel>(user);

        return data;
    }

    public async Task ConfirmEmail(ConfirmationEmailModel model)
    {
        _confirmationEmailModelValidator.CheckValidation(model);

        var user = await _userManager!.FindByEmailAsync(model.Email);
        
        if (user is null)
            throw new Exception($"User with email - {model.Email} doesnt not exists.");

        await _userManager.ConfirmEmailAsync(user, model.Token);
    }

    public async Task<PasswordRecoveryResponse> SendRecoveryPasswordEmail(SendPasswordRecoveryModel model)
    {
        _sendPasswordRecoveryModelValidator.CheckValidation(model);
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null)
            throw new Exception($"--> User with the email({model.Email}) not found!");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var content = PathReader.ReadContent(
                                Path.Combine(Directory.GetCurrentDirectory(), "\\EmailPages\\passwordRecovery.html"),
                                "/app/emailpages/passwordRecovery.html");

        content = content.Replace("QUERYEMAIL", user.Email)
                         .Replace("QUERYTOKEN", token)
                         .Replace("DATENOW", DateTimeOffset.Now.LocalDateTime.ToShortDateString().ToString())
                         ;

        // Sending mail to user email for password recovery
        await _emailSender.SendEmail(new EmailModel()
        {
            EmailTo = user.Email!,
            Subject = "Password recovery message",
            Message = content
        });

        var response = _mapper.Map<PasswordRecoveryResponse>(user); 

        return response;
    }

    public async Task<PasswordRecoveryResponse> RecoverPassword(PasswordRecoveryModel model)
    {
        _passwordRecoveryModelValidator.CheckValidation(model);
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null)
            throw new Exception($"--> User with the email({model.Email}) not found!");

        await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

        var response = _mapper.Map<PasswordRecoveryResponse>(user);

        return response;
    }

    public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordModel model)
    {
        _changePasswordModelValidator.CheckValidation(model);
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null) throw new Exception($"User with email({model.Email}) was not found!");

        /// Compares old password with current
        if (new PasswordHasher<StudentUser>().VerifyHashedPassword(user, user.PasswordHash!, model.CurrentPassword)
            == PasswordVerificationResult.Failed)
            throw new Exception($"Current password is incorrect!");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Changes password
        var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

        if (!result.Succeeded)
            throw new Exception($"Exception by changing password for email({model.Email}).");

        var response = _mapper.Map<ChangePasswordResponse>(user);

        return response;
    }
}
