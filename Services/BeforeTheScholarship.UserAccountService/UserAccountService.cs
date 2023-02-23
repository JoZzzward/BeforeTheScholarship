namespace BeforeTheScholarship.Services.UserAccount;

using AutoMapper;
using BeforeTheScholarship.Common.EmailSettings;
using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Services.EmailSender;
using BeforeTheScholarship.Services.UserAccount.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;

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

    public async Task<UserAccountModel> Create(RegisterUserAccountModel model)
    {
        UserAccountModel data = new();

        if (model.ConfirmPassword != model.Password)
            throw new Exception($"Password and confirm password must be equals");

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
            throw new Exception($"User account with email {model.Email} already exist.");

        user = new StudentUser()
        {
            UserName = "User", 
            FirstName = "",
            LastName = "",
            Email = model.Email,
            EmailConfirmed = false,
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
        };

        if (!string.IsNullOrEmpty(model.Email))
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            _emailSender?.SendEmail(new EmailModel()
            {
                EmailFrom = MainEmail.Email,
                EmailTo = user.Email,
                Subject = $"Hello, dear {user.FirstName}!",
                Message = (Directory.GetCurrentDirectory() + "\\EmailPages\\emailConfirmation.html") ?? "/app/emailspages/debtNotification.html"
            });
        }

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
            data = _mapper.Map<UserAccountModel>(user);

        return data;
    }

    public async Task ConfirmEmail(ConfirmationEmail confirmationEmail)
    {
        var user = await _userManager!.FindByEmailAsync(confirmationEmail.Email);
        _logger.LogInformation($"----> Name of user that was found by email is: {user!.FirstName} and his email is {user.Email}");
        
        if (user is null)
            throw new Exception($"User with email - {confirmationEmail.Email} doesnt not exists.");
        var result = await _userManager.ConfirmEmailAsync(user, confirmationEmail.Token);

        _logger.LogInformation($"----> Result from ConfirmEmail() is: {result.Succeeded}");
    }
}
