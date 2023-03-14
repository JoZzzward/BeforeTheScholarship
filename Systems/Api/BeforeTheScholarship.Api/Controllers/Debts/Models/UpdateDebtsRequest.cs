using AutoMapper;
using BeforeTheScholarship.Services.DebtService;

namespace BeforeTheScholarship.Api.Controllers.Debts;

public class UpdateDebtsRequest
{
    public decimal Borrowed { get; set; }
    public string Phone { get; set; }
    public string BorrowedFromWho { get; set; }
    public DateTimeOffset WhenToPayback { get; set; }
}

public class UpdateDebtsRequestProfile : Profile
{
    public UpdateDebtsRequestProfile()
    {
        CreateMap<UpdateDebtsRequest, UpdateDebtModel>();
    }
}