using AutoMapper;
using BeforeTheScholarship.Services.DebtService;

namespace BeforeTheScholarship.Api.Controllers.Debts;

public class DebtResponse
{
    public int Id { get; set; } 
    public decimal Borrowed { get; set; }
    public string Phone { get; set; }
    public string BorrowedFromWho { get; set; }
    public DateTimeOffset WhenBorrowed { get; set; }
    public DateTimeOffset WhenToPayback { get; set; }
}

public class DebtResponseProfile : Profile
{
    public DebtResponseProfile()
    {
        CreateMap<DebtModel, DebtResponse>();
    }
}