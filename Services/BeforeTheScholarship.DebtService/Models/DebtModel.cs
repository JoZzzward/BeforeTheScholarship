
using AutoMapper;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.DebtService;

public class DebtModel
{
    public int Id { get; set; }
    public Guid StudentId { get; set; }
    public decimal Borrowed { get; set; }
    public string PhoneNumber { get; set; }
    public string BorrowedFromWho { get; set; }
    public bool EmailSended { get; set; }
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
