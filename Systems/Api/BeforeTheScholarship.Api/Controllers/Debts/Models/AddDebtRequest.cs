using AutoMapper;
using BeforeTheScholarship.Services.DebtService;

namespace BeforeTheScholarship.Api.Controllers.Debts;

public class AddDebtRequest
{
    public Guid StudentId { get; set; }
    public decimal Borrowed { get; set; }
    public string Phone { get; set; }
    public string BorrowedFromWho { get; set; }
}

public class AddDebtRequestProfile : Profile
{
    public AddDebtRequestProfile()
    {
        CreateMap<AddDebtRequest, AddDebtModel>();
    }
}