
using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.DebtService;

public class DebtModel
{
    public int Id { get; set; }
    public decimal Borrowed { get; set; }
    public string Phone { get; set; }
    public string BorrowedFromWho { get; set; }
    public DateTime WhenBorrowed { get; set; }
    public DateTime WhenToPayback { get; set; }
}
public class DebtModelProfile : Profile
{
	public DebtModelProfile()
	{
		CreateMap<Debts, DebtModel>();
	}
}
