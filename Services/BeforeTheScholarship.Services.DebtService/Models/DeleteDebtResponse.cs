using AutoMapper;
using BeforeTheScholarship.Entities;
using System.Text.Json.Serialization;

namespace BeforeTheScholarship.Services.DebtService.Models;

public class DeleteDebtResponse
{
    [JsonPropertyName("uid")] public Guid? Uid { get; set; }
}

public class DeleteDebtResponseProfile : Profile
{
    public DeleteDebtResponseProfile()
    {
        CreateMap<Debts, DeleteDebtResponse>();
    }
}