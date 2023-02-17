﻿using AutoMapper;
using BeforeTheScholarship.Context;
using BeforeTheScholarship.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeforeTheScholarship.DebtService;

public class DebtService : IDebtService
{
    private readonly IDbContextFactory<AppDbContext> _dbContext;
    private readonly IMapper _mapper;

    public DebtService(
        IDbContextFactory<AppDbContext> dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DebtModel>> GetDebts(int? studentId)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var debt = context
            .Debts
            .AsQueryable();

        var data = (await debt.ToListAsync()).Where(x => x.StudentId == studentId).Select(s => _mapper.Map<DebtModel>(s))
            ?? new List<DebtModel>();

        return data;
    }

    public async Task<DebtModel> GetDebtById(int? id)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var debt = await context
            .Debts
            .FirstOrDefaultAsync(x => x.Id == id);

        if (debt is null)
            throw new NullReferenceException($"Debt({id}) was not found");

        var data = _mapper.Map<DebtModel>(debt);

        return data;
    }

    public async Task<DebtModel> CreateDebt(AddDebtModel model)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var data = _mapper.Map<Debts>(model);

        await context.Debts.AddAsync(data);
        context.SaveChanges();

        return _mapper.Map<DebtModel>(data);
    }

    public async Task UpdateDebt(int? id, UpdateDebtModel model)
    {
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

    public async Task<IEnumerable<DebtModel>> GetUrgentlyRepaidDebts(int studentId, bool overdue)
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
