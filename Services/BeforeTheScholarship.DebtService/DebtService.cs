using AutoMapper;
using BeforeTheScholarship.Common.Validation;
using BeforeTheScholarship.Context;
using BeforeTheScholarship.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeforeTheScholarship.DebtService;

public class DebtService : IDebtService
{
    private readonly IDbContextFactory<AppDbContext> _dbContext;
    private readonly IMapper _mapper;
    private readonly IModelValidator<AddDebtModel> _addDebtModelValidator;
    private readonly IModelValidator<UpdateDebtModel> _updateDebtModelValidator;

    public DebtService(
        IDbContextFactory<AppDbContext> dbContext,
        IMapper mapper,
        IModelValidator<AddDebtModel> addDebtModelValidator,
        IModelValidator<UpdateDebtModel> updateDebtModelValidator
        )
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _addDebtModelValidator = addDebtModelValidator;
        _updateDebtModelValidator = updateDebtModelValidator;
    }

    public async Task<IEnumerable<DebtModel>> GetDebts()
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var debt = context
            .Debts
            .AsQueryable();

        var data = (await debt.ToListAsync()).Select(s => _mapper.Map<DebtModel>(s))
            ?? new List<DebtModel>();

        return data;
    }

    public async Task<IEnumerable<DebtModel>> GetDebts(Guid? studentId)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var debt = context
            .Debts
            .AsQueryable();

        var data = (await debt.ToListAsync()).Where(x => x.StudentId == studentId).Select(s => _mapper.Map<DebtModel>(s))
            ?? new List<DebtModel>();

        return data;
    }

    public async Task<DebtModel> CreateDebt(AddDebtModel model)
    {
        _addDebtModelValidator.CheckValidation(model);

        using var context = await _dbContext.CreateDbContextAsync();

        var data = _mapper.Map<Debts>(model);

        await context.Debts.AddAsync(data);
        context.SaveChanges();

        return _mapper.Map<DebtModel>(data);
    }

    public async Task UpdateDebt(int? id, UpdateDebtModel model)
    {
        _updateDebtModelValidator.CheckValidation(model);
        using var context = await _dbContext.CreateDbContextAsync();

        var debt = await context
            .Debts
            .FirstOrDefaultAsync(x => x.Id == id);

        if (debt is null)
            throw new NullReferenceException($"Debt({id}) was not found");

        debt = _mapper.Map(model, debt);

        context.Debts.Update(debt);
        context.SaveChanges();
    }

    public async Task DeleteDebt(int? id)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var debt = await context
            .Debts
            .FirstOrDefaultAsync(x => x.Id == id);

        if (debt is null)
            throw new NullReferenceException($"Debt({id}) was not found");

        context.Debts.Remove(debt);
        context.SaveChanges();
    }

    public async Task<IEnumerable<DebtModel>> GetUrgentlyRepaidDebts(Guid studentId, bool overdue)
    {
        var debts = await GetDebts(studentId);

        var daysOff = overdue ? 0 : 3;

        var result = new List<DebtModel>();

        foreach (var debt in debts)
        {
            var subtractDate = debt.WhenToPayback.Subtract(DateTime.Now);

            if (subtractDate.TotalDays <= daysOff && subtractDate.Seconds < 1 && overdue)
                result.Add(debt);
            else if (subtractDate.TotalDays <= daysOff && subtractDate.Seconds >= 0)
                result.Add(debt);
        }

        return _mapper.Map<IEnumerable<DebtModel>>(result);
    }
}
