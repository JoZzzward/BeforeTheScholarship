using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.Services.DebtService.Models;

public class DeleteDebtResponse
{

}

public class DeleteDebtResponseProfile : Profile
{
    public DeleteDebtResponseProfile()
    {
        CreateMap<Debts, DeleteDebtResponse>();
    }
}