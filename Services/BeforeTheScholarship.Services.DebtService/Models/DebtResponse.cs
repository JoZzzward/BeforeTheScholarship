using AutoMapper;
using BeforeTheScholarship.Entities;
using System.Text.Json.Serialization;

namespace BeforeTheScholarship.Services.DebtService;

public class DebtResponse
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("uid")] public Guid Uid { get; set; }
    [JsonPropertyName("borrowed")] public decimal Borrowed { get; set; }
    [JsonPropertyName("phone")] public string Phone { get; set; }
    [JsonPropertyName("borrowedfromwho")] public string BorrowedFromWho { get; set; }
    [JsonPropertyName("whenborrowed")] public DateTimeOffset WhenBorrowed { get; set; }
    [JsonPropertyName("whentopayback")] public DateTimeOffset WhenToPayback { get; set; }
}

public class DebtResponseProfile : Profile
{
    public DebtResponseProfile()
    {
        CreateMap<Debts, DebtResponse>();
    }
}