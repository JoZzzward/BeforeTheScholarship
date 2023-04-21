using BeforeTheScholarship.Web.Pages.Auth;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BeforeTheScholarship.Web;

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

    public async Task<LoginResult> Login(LoginModel loginModel)
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

        var content = await response.Content.ReadAsStringAsync();

        var loginResult = JsonSerializer.Deserialize<LoginResult>(content)
            ?? new LoginResult();
        loginResult.Successful = response.IsSuccessStatusCode;

        if (!response.IsSuccessStatusCode)
            return loginResult;

        await _localStorage.SetItemAsync("authToken", loginResult.AccessToken);
        await _localStorage.SetItemAsync("refreshToken", loginResult.RefreshToken);
        Console.WriteLine(loginModel.Email);
        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email!);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.AccessToken);

        var studentInfo = await GetStudentInfo(loginModel.Email, loginModel.Password);

        await _localStorage.SetItemAsync("studentId", studentInfo.StudentId);

        return loginResult;
    }

    private async Task<LoginUserAccountResponse> GetStudentInfo(string email, string password)
    {
        var url = $"{Settings.ApiRoot}/accounts/login";

        var model = new LoginUserAccountRequest
        {
            Email = email,
            Password = password
        };
        var body = JsonSerializer.Serialize(model);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, request);

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content);

        var result = JsonSerializer.Deserialize<LoginUserAccountResponse>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }

    public async Task<RegisterResponse> Register(RegisterModel registerModel)
    {
        var url = $"{Settings.ApiRoot}/accounts";

        var body = JsonSerializer.Serialize(registerModel);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception(content);

        var data = JsonSerializer.Deserialize<RegisterResponse>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return data;
    }

    public async Task ConfirmEmail(ConfirmationEmail confirmationEmail)
    {
        var url = $"{Settings.ApiRoot}/accounts/confirm-email";
        var body = JsonSerializer.Serialize(confirmationEmail);
        var request = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception(content);
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await _localStorage.RemoveItemAsync("refreshToken");

        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();

        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}
