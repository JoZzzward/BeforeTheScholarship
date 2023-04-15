using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.Services.DebtService.Models;

public class CreateDebtResponse
{
    public Guid StudentId { get; set; }
}
public class CreateDebtResponseProfile : Profile
{
    public CreateDebtResponseProfile()
    {
        CreateMap<Debts, CreateDebtResponse>();
    }
}