using BeforeTheScholarship.Web.Extensions;
using BeforeTheScholarship.Web.Pages.Auth;
using BeforeTheScholarship.Web.Pages.Profile.Models;
using Blazored.LocalStorage;

namespace BeforeTheScholarship.Web.Pages.Profile.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly IAuthService _authService;

        private string url;

        public StudentService(
            HttpClient httpClient,
            ILocalStorageService localStorage,
            IAuthService authService)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authService = authService;

            Task.Run(InitializeDefaultUrlWithStudentId);
        }

        public async Task<StudentUserResponse?> GetStudentUser()
        {
            await _httpClient.CheckServerConnection();

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.ReadContent<StudentUserResponse>();

            return result;
        }

        public async Task<T> UpdateStudentById<T>(UpdateStudentUserRequest data)
        {
            await _httpClient.CheckServerConnection();

            var request = await data.GenerateStringContentRequest();

            var response = await _httpClient.PutAsync(url, request);

            var result = await response.ReadContent<T>();

            return result;
        }

        public async Task<T> DeleteStudentById<T>()
        {
            await _httpClient.CheckServerConnection();

            var response = await _httpClient.DeleteAsync(url);

            var result = await response.ReadContent<T>();

            await _authService.Logout();

            return result;
        }

        private async Task InitializeDefaultUrlWithStudentId()
        {
            var studentId = await _localStorage.GetItemAsStringAsync("studentId");
            studentId = studentId.Replace('\"', ' ').Trim();

            url = $"{Settings.ApiRoot}/students/{studentId}";
        }
    }
}
