using AutoMapper;
using BeforeTheScholarship.Entities;
using System.Text.Json.Serialization;

namespace BeforeTheScholarship.Services.DebtService.Models;

public class UpdateDebtResponse
{
    [JsonPropertyName("uid")] public Guid? Uid { get; set; }
}

public class UpdateDebtResponseProfile : Profile
{
	public UpdateDebtResponseProfile()
	{
		CreateMap<Debts, UpdateDebtResponse>();
	}
}