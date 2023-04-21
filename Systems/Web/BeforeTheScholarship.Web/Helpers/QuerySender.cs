using System.Text.Json;
using BeforeTheScholarship.Web.Pages.Debts.Models;

namespace BeforeTheScholarship.Web.Helpers
{
    public class QuerySender : IQuerySender
    {
        private readonly HttpClient _httpClient;

        public QuerySender(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<DebtListItem>?> SendGetQuery(string url)
        {
            Console.WriteLine("Trying to get data");
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            var debts = JsonSerializer.Deserialize<IEnumerable<DebtListItem>>(content,
                            new JsonSerializerOptions() 
                                { PropertyNameCaseInsensitive = true })
                        ?? new List<DebtListItem>();
            return debts;
        }

    }
}
