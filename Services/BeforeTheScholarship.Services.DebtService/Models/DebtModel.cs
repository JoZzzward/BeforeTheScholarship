
using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.Services.DebtService;

public class DebtModel
{
    public int Id { get; set; }
    public Guid StudentId { get; set; }
    public decimal Borrowed { get; set; }
    public string PhoneNumber { get; set; }
    public string BorrowedFromWho { get; set; }
    public DateTimeOffset WhenBorrowed { get; set; }
    public DateTimeOffset WhenToPayback { get; set; }
}
public class DebtModelProfile : Profile
{
	public DebtModelProfile()
	{
		CreateMap<Debts, DebtModel>();
	}
}
