using System.Text.Json.Serialization;

namespace BeforeTheScholarship.Services.UserAccountService.Models;

public class SendConfirmationEmailResponse
{
    [JsonPropertyName("email")] public string Email { get; set; }
}
