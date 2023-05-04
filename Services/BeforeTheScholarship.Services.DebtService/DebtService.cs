using AutoMapper;
using BeforeTheScholarship.Common.CacheConstKeys;
using BeforeTheScholarship.Common.Exceptions;
using BeforeTheScholarship.Common.Validation;
using BeforeTheScholarship.Context;
using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Services.Actions;
using BeforeTheScholarship.Services.CacheService;
using BeforeTheScholarship.Services.DebtService.Models;
using BeforeTheScholarship.Services.StudentService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BeforeTheScholarship.Services.DebtService;

public class DebtService : Manager, IDebtService
{
    private readonly IDbContextFactory<AppDbContext> _dbContext;
    private readonly ILogger<DebtService> _logger;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    private readonly IModelValidator<CreateDebtModel> _addDebtResponseValidator;
    private readonly IModelValidator<UpdateDebtModel> _updateDebtResponseValidator;

    public DebtService(
        IDbContextFactory<AppDbContext> dbContext,
        IStudentService studentService,
        ILogger<DebtService> logger,
        IMapper mapper,
        IActionsService actionService,
        ICacheService cacheService,
        IModelValidator<CreateDebtModel> addDebtResponseValidator,
        IModelValidator<UpdateDebtModel> updateDebtResponseValidator
        ) : base(studentService, logger, actionService, cacheService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
        _cacheService = cacheService;
        _addDebtResponseValidator = addDebtResponseValidator;
        _updateDebtResponseValidator = updateDebtResponseValidator;
    }

    public async Task<IEnumerable<DebtResponse>?> GetDebts()
    {
        var cachedDataExists = await ReturnCachedDebts(DebtsCacheKeys.AllDebtsKey);

        if (cachedDataExists != null)
        {
            _logger.LogInformation("--> Debts(Count: {DebtsCount}) was successfully returned from cache", cachedDataExists.Count());
            return cachedDataExists;
        }
        using var context = await _dbContext.CreateDbContextAsync();

        var debt = context
            .Debts
            .AsQueryable();

        var response = (await debt.ToListAsync()).Select(_mapper.Map<DebtResponse>);

        if (response is null)
            return null;

        await _cacheService.SetStringAsync(DebtsCacheKeys.AllDebtsKey, response);
        
        _logger.LogInformation("--> Debts(Count: {DebtsCount}) was returned successfully", response.Count());

        return response;
    }

    public async Task<IEnumerable<DebtResponse>?> GetDebts(Guid? studentId)
    {
        var cachedDataExists = await ReturnCachedDebts(DebtsCacheKeys.DebtsWithSpecifiedStudentKey);

        if (cachedDataExists != null)
        {
            _logger.LogInformation("--> Debts(Count: {DebtsCount}) was returned from cache", cachedDataExists.Count());
            return cachedDataExists;
        }

        using var context = await _dbContext.CreateDbContextAsync();

        var student = await context.StudentUsers.FirstOrDefaultAsync(x => x.Id == studentId);

        ProcessException.ThrowIf(() => student is null, $"Student (Id: {studentId}) was not found!");

        var debt = context
            .Debts
            .AsQueryable();

        var response = (await debt.ToListAsync()).Where(x => x.StudentId == studentId).Select(_mapper.Map<DebtResponse>);

        await _cacheService.SetStringAsync(DebtsCacheKeys.DebtsWithSpecifiedStudentKey, response);

        _logger.LogInformation("--> Debts (Count: {DebtsCount}) belong to a Student (Id: {StudentId} was returned successfully.", studentId, response.Count());

        return response;
    }

    public async Task<CreateDebtResponse?> CreateDebt(CreateDebtModel model)
    {
        _addDebtResponseValidator.CheckValidation(model);

        using var context = await _dbContext.CreateDbContextAsync();

        var student = await context.StudentUsers.FirstOrDefaultAsync(x => x.Id == model.StudentId);

        ProcessException.ThrowIf(() => student is null, $"Student (Id: {model.StudentId}) was not found!");

        var data = _mapper.Map<Debts>(model);

        await context.Debts.AddAsync(data);
        await context.SaveChangesAsync();

        await _cacheService.ClearStorage();

        await CreateSendDebtEmailAction(data);

        var response = _mapper.Map<CreateDebtResponse>(data);

        _logger.LogInformation("--> Debt (Id: {DebtId}) was successfully created.", data.Id);

        return response;
    }

    public async Task<UpdateDebtResponse?> UpdateDebt(Guid? uid, UpdateDebtModel model)
    {
        await _updateDebtResponseValidator.CheckValidation(model);
        await using var context = await _dbContext.CreateDbContextAsync();

        var debt = await context
            .Debts
            .FirstOrDefaultAsync(x => x.Uid == uid);

        ProcessException.ThrowIf(() => debt is null, $"Debt (Id: {uid}) was not found!");

        debt = _mapper.Map(model, debt);

        context.Debts.Update(debt);
        await context.SaveChangesAsync();

        await _cacheService.ClearStorage();

        await CreateSendDebtEmailAction(debt);

        var response = _mapper.Map<UpdateDebtResponse?>(debt);

        _logger.LogInformation("--> Debt (Id: {DebtId}) was successfully updated.", uid);

        return response;
    }

    public async Task<DeleteDebtResponse?> DeleteDebt(Guid? uid)
    {
        await using var context = await _dbContext.CreateDbContextAsync();

        var debt = await context
            .Debts
            .FirstOrDefaultAsync(x => x.Uid == uid);

        ProcessException.ThrowIf(() => debt is null, $"Debt (Id: {uid}) was not found!");

        context.Debts.Remove(debt);
        await context.SaveChangesAsync();

        await _cacheService.ClearStorage();

        var response = _mapper.Map<DeleteDebtResponse>(debt);
        
        _logger.LogInformation("--> Debt (Id: {DebtId}) was successfully removed.", uid);

        return response;
    }

    public async Task<IEnumerable<DebtResponse>?> GetUrgentlyRepaidDebts(Guid? studentId)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var student = await context.StudentUsers.FirstOrDefaultAsync(x => x.Id == studentId);

        ProcessException.ThrowIf(() => student is null, $"Student (Id: {studentId}) was not found!");

        var cachedDataExists = await ReturnCachedDebts(DebtsCacheKeys.UrgentlyRepaidDebtsKey);

        if (cachedDataExists != null)
        {
            _logger.LogInformation("--> Urgently repaid debts(Count: {DebtsCount}) was returned from cache", cachedDataExists.Count());
            return cachedDataExists;
        }

        var debts = await GetDebts(studentId);

        if (debts == null)
        {
            _logger.LogInformation("--> User (Id: {StudentId}) dont have any debts.", studentId);
            return null;
        }

        // Generates a list of debts with 1 day or less left to the repayment date
        debts = debts.Where(x => x.WhenToPayback > DateTimeOffset.UtcNow && x.WhenToPayback <= DateTimeOffset.UtcNow.AddDays(1));

        var response = _mapper.Map<IEnumerable<DebtResponse>>(debts);

        _logger.LogInformation("--> The list of urgently repaid debts of the specified student(Id: {StudentId}) has been successfully returned", studentId);

        return response;
    }
    
    public async Task<IEnumerable<DebtResponse>?> GetOverdueDebts(Guid? studentId)
    {
        var cachedDataExists = await ReturnCachedDebts(DebtsCacheKeys.OverdueDebtsKey);

        if (cachedDataExists != null)
        {
            _logger.LogInformation("--> Overdue debts(Count: {DebtsCount}) was returned from cache", cachedDataExists.Count());
            return cachedDataExists;
        }

        var debts = await GetDebts(studentId);

        if (debts == null)
        {
            _logger.LogInformation("--> User (Id: {StudentId}) dont have any debts.", studentId);
            return null;
        }

        // Generates a list of overdue debts
        debts = debts.Where(x => (x.WhenToPayback - DateTimeOffset.Now).TotalDays <= 0);

        var response = _mapper.Map<IEnumerable<DebtResponse>>(debts);

        _logger.LogInformation("--> The list of overdue debts(Count: {DebtsCount}) of the specified student(Id: {StudentId}) has been successfully returned", 
            response.Count(), studentId);

        return response;
    }
}
