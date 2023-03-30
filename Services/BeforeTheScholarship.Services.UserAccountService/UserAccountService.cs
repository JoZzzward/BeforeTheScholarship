using AutoMapper;
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
        private readonly IModelValidator<PasswordRecoveryMailModel> _sendPasswordRecoveryModelValidator;
        private readonly IModelValidator<PasswordRecoveryModel> _passwordRecoveryModelValidator;
        private readonly IModelValidator<ChangePasswordModel> _changePasswordModelValidator;

        public UserAccountService(
            IMapper mapper,
            UserManager<StudentUser> userManager,
            SignInManager<StudentUser> signInManager,
            IEmailSender emailSender,
            ILogger<UserAccountService> logger,
            IModelValidator<RegisterUserAccountModel> registerModelValidator,
            IModelValidator<LoginUserAccountModel> loginModelValidator,
            IModelValidator<ConfirmationEmailModel> confirmationEmailModelValidator,
            IModelValidator<PasswordRecoveryMailModel> sendPasswordRecoveryModelValidator,
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
            _loginModelValidator = loginModelValidator;
            _confirmationEmailModelValidator = confirmationEmailModelValidator;
            _sendPasswordRecoveryModelValidator = sendPasswordRecoveryModelValidator;
            _passwordRecoveryModelValidator = passwordRecoveryModelValidator;
            _changePasswordModelValidator = changePasswordModelValidator;
        }

        public async Task<RegisterUserAccountResponse> RegisterUser(RegisterUserAccountModel model)
        {
            _registerModelValidator.CheckValidation(model);

            var user = await _userManager.FindByEmailAsync(model.Email.RemoveWhiteSpaces());

            if (user != null)
            {
                _logger.LogError("--> ERROR: User with email {UserEmail} already exist.", model.Email);
                throw new Exception($"User account with email {model.Email} already exist.");
            }

            user = _mapper.Map<StudentUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errorList = string.Join(", ", result.Errors.Select(s => s.Description));

                _logger.LogError("--> ERROR: User with email({UserEmail}) cannot be registered: {ErrorList}", model.Email, errorList);
                throw new Exception($"Error at user registration:  {errorList}");
            }

            await SendEmailConfirmationMail(user);

            var response = _mapper.Map<RegisterUserAccountResponse>(user);

            _logger.LogInformation("--> User(Email: {UserEmail}) successfully registered", response.Email);

            return response;
        }

        public async Task<LoginUserAccountResponse> LoginUser(LoginUserAccountModel model)
        {
            _loginModelValidator.CheckValidation(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                _logger.LogError("--> ERROR: User with email {UserEmail} not found while login in.", model.Email);
                throw new Exception($"User with email {model.Email} not found.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (!result.Succeeded)
                throw new Exception($"Invalid email or password.");

            var response = _mapper.Map<LoginUserAccountResponse>(user);

            _logger.LogInformation("--> User(Email: {UserEmail}) successfully logged in.", response.Email);

            return response;
        }

        public async Task<ConfirmationEmailResponse> ConfirmEmail(ConfirmationEmailModel model)
        {
            _confirmationEmailModelValidator.CheckValidation(model);

            var user = await _userManager!.FindByEmailAsync(model.Email);

            // TODO: Replace exception with "return null" expression
            if (user is null)
            {
                _logger.LogError("--> ERROR: User (Email: {UserEmail}) does not exists.", model.Email);
                throw new Exception($"User with email - {model.Email} does not exists.");
            }

            await _userManager.ConfirmEmailAsync(user, model.Token);

            var response = _mapper.Map<ConfirmationEmailResponse>(model);

            _logger.LogInformation("--> User with email({UserEmail}) successfully confirmed his email", response.Email);

            return response;
        }

        public async Task<PasswordRecoveryResponse> SendRecoveryPasswordEmail(PasswordRecoveryMailModel model)
        {
            _sendPasswordRecoveryModelValidator.CheckValidation(model);
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                _logger.LogError("--> ERROR: User (Email: {UserEmail}) was not found.", model.Email);
                throw new Exception($"User (Email: {model.Email}) not found!");
            }

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

        public async Task<PasswordRecoveryResponse> RecoverPassword(PasswordRecoveryModel model)
        {
            _passwordRecoveryModelValidator.CheckValidation(model);
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                _logger.LogError("--> ERROR: User (Email: {UserEmail}) was not found.", model.Email);
                throw new Exception($"User (Email: {model.Email}) not found!");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!result.Succeeded)
                throw new Exception($"Exception by recovering password for Email ({model.Email}).");

            var response = _mapper.Map<PasswordRecoveryResponse>(user);

            _logger.LogInformation("--> Password of User(Email: {UserEmail}) was successfully recovered.", response.Email);

            return response;
        }

        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordModel model)
        {
            _changePasswordModelValidator.CheckValidation(model);
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                _logger.LogError("--> ERROR: User (Email: {UserEmail}) was not found.", model.Email);
                throw new Exception($"User (Email: {model.Email}) not found!");
            }

            // Compares old password with current
            if (new PasswordHasher<StudentUser>().VerifyHashedPassword(user, user.PasswordHash!, model.CurrentPassword)
                == PasswordVerificationResult.Failed)
                throw new Exception($"Current password is incorrect!");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Changes password
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (!result.Succeeded)
                throw new Exception($"Exception by changing password for Email ({model.Email}).");

            var response = _mapper.Map<ChangePasswordResponse>(user);

            _logger.LogInformation("--> Password of User (Email: {UserEmail}) was successfully changed.", response.Email);

            return response;
        }

        private async Task SendEmailConfirmationMail(StudentUser user)
        {
            if (user.Email == null)
            {
                _logger.LogInformation("--> Email confirmation mail for user(UserName: {UserUserName}) not was sent. Email is empty.", user.UserName);
                return;
            }

            _logger.LogInformation("--> Trying to send message with email confirmation link to user (Email: {UserEmail})", user.Email);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var content = ContentReader.ReadFromFile("emailConfirmation.html", user.Email, token);

            // Setting first letter to uppercase
            var username = char.ToUpper(user.UserName[0]) + user.UserName.Substring(1);

            // Sending email confirmation mail for current user
            _emailSender?.SendEmail(new EmailModel()
            {
                EmailTo = user.Email,
                Subject = $"Hello, dear {username}!",
                Message = content
            });

            _logger.LogInformation("--> Email confirmation message was sent to user (Email: {UserEmail})", user.Email);
        }
    }
}