using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.DebtService;

public class UpdateDebtModel
{
    public decimal Borrowed { get; set; }
    public string Phone { get; set; }
    public string BorrowedFromWho { get; set; }
    public DateTime WhenToPayback { get; set; }
}

public class UpdateDebtModelProfile : Profile
{
	public UpdateDebtModelProfile()
	{
		CreateMap<UpdateDebtModel, Debts>();
	}
}
