using System.Text;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BeforeTheScholarship.Tests.Integration.Base.Helpers
{
    public abstract class DataHelper
    {
        public StringContent GenerateRequestFromModel<T>(T model)
        {
            var body = JsonSerializer.Serialize(model);
            var request = new StringContent(body, Encoding.UTF8, "application/json");

            return request;
        }

        public T GenerateContentFromModel<T>(HttpResponseMessage response)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var content = JsonSerializer.Deserialize<T>(responseContent);

            return content;
        }
    }
}
