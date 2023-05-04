using AutoMapper;
using BeforeTheScholarship.Common.Exceptions;
using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Common.Validation;
using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Services.EmailSender;
using BeforeTheScholarship.Services.UserAccountService.Helpers;
using BeforeTheScholarship.Services.UserAccountService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BeforeTheScholarship.Services.UserAccountService
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<StudentUser> _userManager;
        private readonly SignInManager<StudentUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<UserAccountService> _logger;
        private readonly IModelValidator<RegisterUserAccountModel> _registerModelValidator;
        private readonly IModelValidator<LoginUserAccountModel> _loginModelValidator;
        private readonly IModelValidator<ConfirmationEmailModel> _confirmationEmailModelValidator;
        private readonly IModelValidator<SendPasswordRecoveryModel> _sendPasswordRecoveryModelValidator;
        private readonly IModelValidator<PasswordRecoveryModel> _passwordRecoveryModelValidator;
        private readonly IModelValidator<ChangePasswordModel> _changePasswordModelValidator;
        private readonly IModelValidator<SendConfirmationEmailModel> _sendConfirmationEmailModelValidator;

        public UserAccountService(
            IMapper mapper,
            UserManager<StudentUser> userManager,
            SignInManager<StudentUser> signInManager,
            IEmailSender emailSender,
            ILogger<UserAccountService> logger,
            IModelValidator<RegisterUserAccountModel> registerModelValidator,
            IModelValidator<SendConfirmationEmailModel> sendConfirmationEmailModelValidator,
            IModelValidator<LoginUserAccountModel> loginModelValidator,
            IModelValidator<ConfirmationEmailModel> confirmationEmailModelValidator,
            IModelValidator<SendPasswordRecoveryModel> sendPasswordRecoveryModelValidator,
            IModelValidator<PasswordRecoveryModel> passwordRecoveryModelValidator,
            IModelValidator<ChangePasswordModel> changePasswordModelValidator
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _registerModelValidator = registerModelValidator;
            _sendConfirmationEmailModelValidator = sendConfirmationEmailModelValidator;
            _loginModelValidator = loginModelValidator;
            _confirmationEmailModelValidator = confirmationEmailModelValidator;
            _sendPasswordRecoveryModelValidator = sendPasswordRecoveryModelValidator;
            _passwordRecoveryModelValidator = passwordRecoveryModelValidator;
            _changePasswordModelValidator = changePasswordModelValidator;
        }

        public async Task<RegisterUserAccountResponse?> RegisterUser(RegisterUserAccountModel model)
        {
            _registerModelValidator.CheckValidation(model);

            var user = await _userManager.FindByEmailAsync(model.Email.RemoveWhiteSpaces());

            user ??= await _userManager.FindByNameAsync(model.Email.RemoveWhiteSpaces());

            if (user != null)
            {
                _logger.LogError("--> User (Email: {UserEmail}) with specific credentials already exist.", model.Email);
                throw new ProcessException($"User (Email: {model.Email}) with specific credentials already exist.");
            }

            user = _mapper.Map<StudentUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (!result.Succeeded)
            {
                var errorList = string.Join(", ", result.Errors.Select(s => s.Description));

                _logger.LogError("--> User (Email: {UserEmail}) can not be registered. Errors: {ErrorList}", model.Email, errorList);
                throw new ProcessException($"User (Email: {model.Email}) can not be registered. Errors: {errorList}");
            }

            await SendEmailConfirmationMail(user);

            var response = _mapper.Map<RegisterUserAccountResponse>(user);

            _logger.LogInformation("--> User (Email: {UserEmail}) successfully registered", response.Email);

            return response;
        }

        public async Task<LoginUserAccountResponse?> LoginUser(LoginUserAccountModel model)
        {
            _loginModelValidator.CheckValidation(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                user = await _userManager.FindByNameAsync(model.Email);

                if (user is null)
                {
                    _logger.LogError("--> User (Email: {UserEmail}) not found while login in.", model.Email);
                    throw new ProcessException($"User (Email: {model.Email}) not found while login in.");
                }
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            ProcessException.ThrowIf(() => !result.Succeeded, $"User (Email: {model.Email}) can not signed in.");

            var response = _mapper.Map<LoginUserAccountResponse>(user);

            _logger.LogInformation("--> User (Email: {UserEmail}) successfully logged in.", response.Email);

            return response;
        }

        public async Task<SendConfirmationEmailResponse?> SendConfirmEmail(SendConfirmationEmailModel model)
        {
            _sendConfirmationEmailModelValidator.CheckValidation(model);
            
            var user = await _userManager.FindByEmailAsync(model.Email);

            var resultSucceeded = await SendEmailConfirmationMail(user);

            if (!resultSucceeded)
                return null;

            var response = _mapper.Map<SendConfirmationEmailResponse>(model);

            return response;
        }

        public async Task<ConfirmationEmailResponse?> ConfirmEmail(ConfirmationEmailModel model)
        {
            _confirmationEmailModelValidator.CheckValidation(model);

            var user = await _userManager!.FindByEmailAsync(model.Email);

            ProcessException.ThrowIf(() => user == null, $"User (Email: {model.Email}) does not exists.");

            var result = await _userManager.ConfirmEmailAsync(user, model.Token);

            ProcessException.ThrowIf(
                () => !result.Succeeded, 
                $"User (Email: {model.Email}) can not confirm his email. Errors: {
                string.Join(", ", result.Errors.Select(s => s.Description))}");

            var response = _mapper.Map<ConfirmationEmailResponse>(model);

            _logger.LogInformation("--> User (Email: {UserEmail}) successfully confirmed his email.", response.Email);

            return response;
        }

        public async Task<PasswordRecoveryResponse?> SendRecoveryPasswordEmail(SendPasswordRecoveryModel model)
        {
            _sendPasswordRecoveryModelValidator.CheckValidation(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            ProcessException.ThrowIf(() => user == null, $"User (Email: {model.Email}) was not found.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var content = ContentReader.ReadFromFile("passwordRecovery.html", user.Email, token);

            // Sending mail to user email for password recovery
            await _emailSender.SendEmail(new EmailModel
            {
                EmailTo = user.Email!,
                Subject = "Password recovery message",
                Message = content
            });

            var response = _mapper.Map<PasswordRecoveryResponse>(user);

            _logger.LogInformation("--> Password message successfully was sent to User (Email: {UserEmail})", response.Email);

            return response;
        }

        public async Task<PasswordRecoveryResponse?> RecoverPassword(PasswordRecoveryModel model)
        {
            _passwordRecoveryModelValidator.CheckValidation(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            ProcessException.ThrowIf(() => user == null, $"User (Email: {model.Email}) was not found.");

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            ProcessException.ThrowIf(
                () => !result.Succeeded, 
                $"User (Email: {model.Email}) can not recover his password. Errors: {string.Join(", ", result.Errors.Select(s => s.Description))}");

            var response = _mapper.Map<PasswordRecoveryResponse>(user);

            _logger.LogInformation("--> Password of User(Email: {UserEmail}) was successfully recovered.", response.Email);

            return response;
        }

        public async Task<ChangePasswordResponse?> ChangePassword(ChangePasswordModel model)
        {
            _changePasswordModelValidator.CheckValidation(model);
            var user = await _userManager.FindByEmailAsync(model.Email);

            ProcessException.ThrowIf(() => user == null, $"User (Email: {model.Email}) was not found.");

            // Compares old password with current
            var passwordVerifiedStatus =
                new PasswordHasher<StudentUser>().VerifyHashedPassword(user, user.PasswordHash!, model.CurrentPassword);

            ProcessException.ThrowIf(
                () => passwordVerifiedStatus == PasswordVerificationResult.Failed, 
                "Current password is incorrect!");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Changes password
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            ProcessException.ThrowIf(
                () => !result.Succeeded, 
                $"User (Email: {model.Email}) can not change his password. Errors: {string.Join(", ", result.Errors.Select(s => s.Description))}");

            var response = _mapper.Map<ChangePasswordResponse>(user);

            _logger.LogInformation("--> Password of User (Email: {UserEmail}) was successfully changed.", response.Email);

            return response;
        }

        private async Task<bool> SendEmailConfirmationMail(StudentUser? user)
        {
            ProcessException.ThrowIf(() => user?.Email == null, "Email confirmation mail for user was not sent. Email is Empty.");

            _logger.LogInformation("--> Trying to send message with email confirmation link to user (Email: {UserEmail})", user.Email);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var content = ContentReader.ReadFromFile("emailConfirmation.html", user.Email, token);

            // Setting first letter to uppercase
            var username = char.ToUpper(user.UserName[0]) + user.UserName[1..];

            // Sending email confirmation mail for current user
            _emailSender?.SendEmail(new EmailModel
            {
                EmailTo = user.Email,
                Subject = $"Hello, dear {username}!",
                Message = content
            });

            _logger.LogInformation("--> Email confirmation message was sent to user (Email: {UserEmail})", user.Email);

            return true;
        }
    }
}