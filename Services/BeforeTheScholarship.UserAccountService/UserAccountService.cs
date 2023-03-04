namespace BeforeTheScholarship.Services.UserAccount;

using AutoMapper;
using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Services.EmailSender;
using BeforeTheScholarship.UserAccountService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

public class UserAccountService : IUserAccountService
{
    private readonly IMapper _mapper;
    private readonly UserManager<StudentUser> _userManager;
    private readonly ILogger<UserAccountService> _logger;
    private readonly IEmailSender _emailSender;

    public UserAccountService(
        IMapper mapper,
        UserManager<StudentUser> userManager,
        ILogger<UserAccountService> logger,
        IEmailSender emailSender
    )
    {
        _mapper = mapper;
        _userManager = userManager; 
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task<UserAccountModel> RegisterUser(RegisterUserAccountModel model)
    {
        if (model.ConfirmPassword != model.Password)
            throw new Exception($"Password and confirm password must be equals");

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

            // Gets a path of html page for mail content
            var path = $"{Directory.GetCurrentDirectory()}\\EmailPages\\emailConfirmation.html";
            
            string content;

            // Filling content
            // TODO: Fix this big blog with reading
            try
            {
                content = File.ReadAllText(path);
            }
            catch
            {
                path = "/app/emailpages/emailConfirmation.html";
                content = File.ReadAllText(path);
            }

            content = content.Replace("QUERYEMAIL", user.Email)
                         .Replace("QUERYTOKEN", token)
                         .Replace("DATENOW", DateTime.Now.ToLocalTime().ToShortDateString().ToString())
                         ;


            // Sending mail to user email for confirmation
            _emailSender?.SendEmail(new EmailModel()
            {
                EmailTo = user.Email,
                Subject = $"Hello, dear {char.ToUpper(user.UserName[0]) + user.UserName.Substring(1)}!",
                Message = content
            });
        }

        return _mapper.Map<UserAccountModel>(user);
    }

    public async Task ConfirmEmail(ConfirmationEmailModel confirmationEmail)
    {
        var user = await _userManager!.FindByEmailAsync(confirmationEmail.Email);
        
        if (user is null)
            throw new Exception($"User with email - {confirmationEmail.Email} doesnt not exists.");

        await _userManager.ConfirmEmailAsync(user, confirmationEmail.Token);
    }

    public async Task<PasswordRecoveryResponse> SendRecoveryPasswordEmail(SendPasswordRecoveryModel request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new Exception($"--> User with the email({request.Email}) not found!");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Gets a path of html page for mail content
        var path = $"{Directory.GetCurrentDirectory()}\\EmailPages\\passwordRecovery.html";
        
        string content;

        // Filling content
        try
        {
            content = File.ReadAllText(path);
        }
        catch
        {
            path = "/app/emailpages/passwordRecovery.html";
            content = File.ReadAllText(path);
        }

        content = content.Replace("QUERYEMAIL", user.Email)
                         .Replace("QUERYTOKEN", token)
                         .Replace("DATENOW", DateTimeOffset.Now.LocalDateTime.ToShortDateString().ToString())
                         ;

        // Sending mail to user email for password recovery
        await _emailSender.SendEmail(new EmailModel()
        {
            EmailTo = user.Email ?? request.Email,
            Subject = "Password recovery message",
            Message = content
        });

        var result = _mapper.Map<PasswordRecoveryResponse>(user); 

        return result;
    }

    public async Task<PasswordRecoveryResponse> RecoverPassword(PasswordRecoveryModel request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new Exception($"--> User with the email({request.Email}) not found!");

        await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        var result = _mapper.Map<PasswordRecoveryResponse>(user);

        return result;
    }

    public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordModel request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null) throw new Exception($"User with email({request.Email}) was not found!");

        /// Compares old password with current
        if (new PasswordHasher<StudentUser>().VerifyHashedPassword(user, user.PasswordHash!, request.CurrentPassword)
            == PasswordVerificationResult.Failed)
            throw new Exception($"Current password is incorrect!");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Changes password
        var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

        if (!result.Succeeded)
            throw new Exception($"Exception on changing current password to new");

        var response = _mapper.Map<ChangePasswordResponse>(user);

        return response;
    }
}
