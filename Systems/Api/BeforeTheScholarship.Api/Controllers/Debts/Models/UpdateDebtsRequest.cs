using AutoMapper;

namespace BeforeTheScholarship.Api.Controllers.Debts;

public class UpdateDebtsRequest
{
    public decimal Borrowed { get; set; }
    public string Phone { get; set; }
    public string BorrowedFromWho { get; set; }
    public DateTime WhenToPayback { get; set; }
}

public class UpdateDebtsRequestProfile : Profile
{
    public UpdateDebtsRequestProfile()
    {
        CreateMap<UpdateDebtsRequest, DebtService.UpdateDebtModel>();
    }
}