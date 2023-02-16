using AutoMapper;

namespace BeforeTheScholarship.Api.Controllers.Debts;

public class AddDebtRequest
{
    public int StudentId { get; set; }
    public decimal Borrowed { get; set; }
    public string Phone { get; set; }
    public string BorrowedFromWho { get; set; }
}

public class AddDebtRequestProfile : Profile
{
    public AddDebtRequestProfile()
    {
        CreateMap<AddDebtRequest, DebtService.AddDebtModel>();
    }
}