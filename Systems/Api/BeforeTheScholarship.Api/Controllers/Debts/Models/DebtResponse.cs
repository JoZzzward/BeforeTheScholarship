using AutoMapper;

namespace BeforeTheScholarship.Api.Controllers.Debts.Models;

public class DebtResponse
{
    public int Id { get; set; }
    public decimal Borrowed { get; set; }
    public string Phone { get; set; }
    public string BorrowedFromWho { get; set; }
    public DateTime WhenBorrowed { get; set; }
    public DateTime WhenToPayback { get; set; }
}

public class DebtResponseProfile : Profile
{
    public DebtResponseProfile()
    {
        CreateMap<DebtService.DebtModel, DebtResponse>();
    }
}