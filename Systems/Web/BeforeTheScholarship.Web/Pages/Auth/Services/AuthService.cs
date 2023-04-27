using BeforeTheScholarship.Web;
using BeforeTheScholarship.Web.Extensions;
using BeforeTheScholarship.Web.Pages.Auth;
using BeforeTheScholarship.Web.Pages.Auth.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text.Json;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient httpClient,
                       AuthenticationStateProvider authenticationStateProvider,
                       ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _localStorage = localStorage;
    }

    public async Task<LoginResult> Login(LoginUserAccountRequest loginModel)
    {
        var url = $"{Settings.IdentityRoot}/connect/token";

        var request_body = new[] 
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", Settings.ClientId),
            new KeyValuePair<string, string>("client_secret", Settings.ClientSecret),
            new KeyValuePair<string, string>("username", loginModel.Email!),
            new KeyValuePair<string, string>("password", loginModel.Password!)
        };

        var requestContent = new FormUrlEncodedContent(request_body);

        var response = await _httpClient.PostAsync(url, requestContent);

        var loginResult = await response.ReadContent<LoginResult>();

        loginResult.Successful = response.IsSuccessStatusCode;

        if (!response.IsSuccessStatusCode)
            return loginResult;

        await _localStorage.SetItemAsync("authToken", loginResult.AccessToken);
        await _localStorage.SetItemAsync("refreshToken", loginResult.RefreshToken);

        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email!);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.AccessToken);

        var studentInfo = await LoginUser(loginModel.Email, loginModel.Password);

        await _localStorage.SetItemAsync("studentId", studentInfo.StudentId);

        return loginResult;
    }

    public async Task<RegisterUserAccountResponse> Register(RegisterUserAccountRequest registerModel)
    {
        var url = $"{Settings.ApiRoot}/accounts/register";

        var request = await registerModel.GenerateStringContentRequest();

        var response = await _httpClient.PostAsync(url, request);

        var result = await response.ReadContent<RegisterUserAccountResponse>();

        return result;
    }

    public async Task<SendConfirmationEmailResponse> SendConfirmEmail(SendConfirmationEmailRequest model)
    {
        var url = $"{Settings.ApiRoot}/accounts/send-confirm-email";

        var request = await model.GenerateStringContentRequest();

        var response = await _httpClient.PostAsync(url, request);

        var result = await response.ReadContent<SendConfirmationEmailResponse>();

        return result;
    }

    public async Task<ConfirmationEmailResponse> ConfirmEmail(ConfirmationEmailRequest confirmationEmail)
    {
        var url = $"{Settings.ApiRoot}/accounts/confirm-email";

        var request = await confirmationEmail.GenerateStringContentRequest();

        var response = await _httpClient.PostAsync(url, request);

        var result = await response.ReadContent<ConfirmationEmailResponse>();

        return result;
    }

    public async Task<PasswordRecoveryResponse> RecoverPassword(PasswordRecoveryRequest passwordRecoveryRequest)
    {
        var url = $"{Settings.ApiRoot}/accounts/recover-password";

        var request = await passwordRecoveryRequest.GenerateStringContentRequest();

        var response = await _httpClient.PostAsync(url, request);

        var result = await response.ReadContent<PasswordRecoveryResponse>();

        return result;
    }

    public async Task<PasswordRecoveryResponse> SendRecoverPasswordMail(SendPasswordRecoveryRequest sendPasswordRecoveryRequest)
    {
        var url = $"{Settings.ApiRoot}/accounts/send-recover-password";

        var request = await sendPasswordRecoveryRequest.GenerateStringContentRequest();

        var response = await _httpClient.PostAsync(url, request);

        var result = await response.ReadContent<PasswordRecoveryResponse>();

        return result;
    }

    public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest changePasswordRequest)
    {
        var url = $"{Settings.ApiRoot}/accounts/change-password";

        var request = await changePasswordRequest.GenerateStringContentRequest();
        var response = await _httpClient.PostAsync(url, request);

        var result = await response.ReadContent<ChangePasswordResponse>();

        return result;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await _localStorage.RemoveItemAsync("refreshToken");
        await _localStorage.RemoveItemAsync("studentId");

        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();

        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
    private async Task<LoginUserAccountResponse> LoginUser(string email, string password)
    {
        var url = $"{Settings.ApiRoot}/accounts/login";

        var model = new LoginUserAccountRequest
        {
            Email = email,
            Password = password
        };

        var request = await model.GenerateStringContentRequest();

        var response = await _httpClient.PostAsync(url, request);

        var result = await response.ReadContent<LoginUserAccountResponse>();

        return result;
    }
}
