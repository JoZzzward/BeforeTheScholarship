using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.Services.DebtService.Models;

public class UpdateDebtResponse
{

}

public class UpdateDebtResponseProfile : Profile
{
	public UpdateDebtResponseProfile()
	{
		CreateMap<Debts, UpdateDebtResponse>();
	}
}