using BeforeTheScholarship.Web.Extensions;
using BeforeTheScholarship.Web.Pages.Debts.Models;
using Blazored.LocalStorage;

namespace BeforeTheScholarship.Web.Pages.Debts.Services
{
    public class DebtService : IDebtService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public DebtService(HttpClient httpClient,
            ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<IEnumerable<DebtListItem>?> GetDebtsByStudentIdAsync(string additionalUrl = "")
        {
            await _httpClient.CheckServerConnection();

            var studentId = await _localStorage.GetItemAsStringAsync("studentId");
            studentId = studentId.Replace('\"', ' ').Trim();

            if (string.IsNullOrEmpty(studentId))
                return null;

            var url = $"{Settings.ApiRoot}/debts/{additionalUrl}{studentId}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.ReadContent<IEnumerable<DebtListItem>?>();

            return result;
        }

        public async Task<T> SendPostQuery<T>(string url, object data)
        {
            await _httpClient.CheckServerConnection();

            var request = await data.GenerateStringContentRequest();

            var response = await _httpClient.PostAsync(url, request);

            var result = await response.ReadContent<T>();

            return result;
        }

        public async Task<T> SendPutQuery<T>(string url, object data)
        {
            await _httpClient.CheckServerConnection();

            var request = await data.GenerateStringContentRequest();

            var response = await _httpClient.PutAsync(url, request);

            var result = await response.ReadContent<T>();

            return result;
        }

        public async Task<T> SendDeleteQuery<T>(string url)
        {
            await _httpClient.CheckServerConnection();

            var response = await _httpClient.DeleteAsync(url);

            var result = await response.ReadContent<T>();

            return result;
        }
    }
}
