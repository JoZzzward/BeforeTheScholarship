using System.Text.Json.Serialization;

namespace BeforeTheScholarship.Web.Pages.Auth;

public class ConfirmationEmail
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("token")]
    public string Token { get; set; }
}
