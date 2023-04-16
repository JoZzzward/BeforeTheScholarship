using AutoMapper;
using BeforeTheScholarship.Entities;
using System.Text.Json.Serialization;

namespace BeforeTheScholarship.Services.DebtService.Models;

public class CreateDebtResponse
{
    [JsonPropertyName("studentid")] public Guid StudentId { get; set; }
}
public class CreateDebtResponseProfile : Profile
{
    public CreateDebtResponseProfile()
    {
        CreateMap<Debts, CreateDebtResponse>();
    }
}