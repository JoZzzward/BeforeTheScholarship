using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.DebtService;

public class AddDebtModel
{
    public int StudentId { get; set; }  
    public decimal Borrowed { get; set; }
    public string Phone { get; set; }
    public string BorrowedFromWho { get; set; }
}

public class AddDebtModelProfile : Profile
{
	public AddDebtModelProfile()
	{
		CreateMap<AddDebtModel, Debts>();
    }
}
