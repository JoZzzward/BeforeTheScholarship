using System.Text;
using System.Text.Json;

namespace BeforeTheScholarship.Tests.Integration.Base.Helpers
{
    internal abstract class DataHelper
    {
        public StringContent GenerateRequestFromModel<T>(T model)
        {
            var body = JsonSerializer.Serialize(model);
            var request = new StringContent(body, Encoding.UTF8, "application/json");

            return request;
        }

        public T GenerateRequestFromModel<T>(HttpResponseMessage response)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var content = JsonSerializer.Deserialize<T>(responseContent);

            return content;
        }

        public virtual Task<string> GenerateUserConfirmationToken(string email) => Task.FromResult(string.Empty);
        public virtual Task<string> GenerateUserRecoveryPasswordToken(string email) => Task.FromResult(string.Empty);
    }
}
