using System.Text;
using System.Text.Json;

namespace BeforeTheScholarship.Web.Extensions
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Checks connections to server by sending default query
        /// </summary>
        /// <exception cref="HttpRequestException">Throws <see cref="HttpRequestException"/> if server is not available</exception>
        public static async Task CheckServerConnection(this HttpClient httpClient)
        {
            try
            {
                await httpClient.GetAsync($"{Settings.ApiRoot}/debts");
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Can not fetch server error. Message: {ex.Message}");
            }
        }

        public static async Task<T> ReadContent<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<T>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }

        public static async Task<StringContent> GenerateStringContentRequest(this object data)
        {
            var body = JsonSerializer.Serialize(data);

            var request = new StringContent(body, Encoding.UTF8, "application/json");

            return request;
        }
    }
}
