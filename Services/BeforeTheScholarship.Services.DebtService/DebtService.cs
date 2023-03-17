using AutoMapper;
using BeforeTheScholarship.Common.CacheConstKeys;
using BeforeTheScholarship.Common.Extensions;
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
        ) : base (dbContext, studentService, logger, mapper, actionService, cacheService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
        _cacheService = cacheService;
        _addDebtResponseValidator = addDebtResponseValidator;
        _updateDebtResponseValidator = updateDebtResponseValidator;
    }

    public async Task<IEnumerable<DebtResponse>> GetDebts()
    {
        var cachedDataExists = await ReturnCachedDebts(DebtsCacheKeys.AllDebtsKey);

        if (cachedDataExists != null)
        {
            _logger.LogInformation("Debts(Count: {DebtsCount}) was successfully returned from cache", cachedDataExists.Count());
            return cachedDataExists;
        }

        var response = await GetDebtsResponse();
       
        _logger.LogInformation("Debts(Count: {DebtsCount}) was returned successfully", response.Count());

        await _cacheService.SetStringAsync(DebtsCacheKeys.AllDebtsKey, response);

        return response;
    }

    public async Task<IEnumerable<DebtResponse>> GetDebts(Guid? studentId)
    {
        var cachedDataExists = await ReturnCachedDebts(DebtsCacheKeys.DebtsWithSpecifiedStudentKey);

        if (cachedDataExists != null)
        {
            _logger.LogInformation("Debts(Count: {DebtsCount}) was returned from cache", cachedDataExists.Count());
            return cachedDataExists;
        }

        var response = await GetDebtsResponse(studentId);

        _logger.LogInformation("Debts(Count: {DebtsCount}) was returned successfully", response.Count());

        await _cacheService.SetStringAsync(DebtsCacheKeys.DebtsWithSpecifiedStudentKey, response);

        return response;
    }

    // TODO: Think about optimization. Divide this method on two different. One with overdue and one without
    public async Task<IEnumerable<DebtResponse>> GetUrgentlyRepaidDebts(Guid studentId, bool overdue)
    {
        var cachedDataExists = await ReturnCachedDebts(DebtsCacheKeys.UrgentlyRepaidDebtsKey);

        if (cachedDataExists != null)
        {
            _logger.LogInformation("Urgently repaid debts({DebtsCount}) was returned from cache", cachedDataExists.Count());
            return cachedDataExists;
        }

        var debts = await GetDebts(studentId);

        var daysOff = overdue ? 0 : 3;

        var result = new List<DebtResponse>();

        foreach (var debt in debts)
        {
            var subtractDate = debt.WhenToPayback.Subtract(DateTime.Now.ToLocalTime());

            if (subtractDate.TotalDays <= daysOff && subtractDate.Seconds < 1 && overdue)
                result.Add(debt);
            else if (subtractDate.TotalDays <= daysOff && subtractDate.Seconds >= 0)
                result.Add(debt);
        }
        var response = _mapper.Map<IEnumerable<DebtResponse>>(result);
        
        _logger.LogInformation("--> Student(Id: {StudentId}) debts that need to be repaid urgently or that were overdue have been successfully returned.", studentId);

        return response;
    }

    public async Task<CreateDebtResponse> CreateDebt(CreateDebtModel model)
    {
        _addDebtResponseValidator.CheckValidation(model);

        using var context = await _dbContext.CreateDbContextAsync();

        var data = _mapper.Map<Debts>(model);

        await context.Debts.AddAsync(data);
        context.SaveChanges();

        await _cacheService.ClearStorage();

        await CreateDebtSendEmailAction(data);

        var response = _mapper.Map<CreateDebtResponse>(data);

        _logger.LogInformation("--> Debt(Id: {DebtId}) was successfully created.", data.Id);

        return response;
    }      

    public async Task<UpdateDebtResponse> UpdateDebt(int? id, UpdateDebtModel model)
    {
        _updateDebtResponseValidator.CheckValidation(model);
        using var context = await _dbContext.CreateDbContextAsync();

        var debt = await context
            .Debts
            .FirstOrDefaultAsync(x => x.Id == id);

        if (debt is null)
        {
            _logger.LogError("Debt(Id: {DebtId}) was not found", id);
            throw new NullReferenceException($"Debt({id}) was not found");
        }

        debt = _mapper.Map(model, debt);

        context.Debts.Update(debt);
        context.SaveChanges();

        await _cacheService.ClearStorage();

        await CreateDebtSendEmailAction(debt);

        var response = _mapper.Map<UpdateDebtResponse>(debt);

        _logger.LogInformation("--> Debt(Id: {DebtId}) was successfully updated.", id);

        return response;
    }

    public async Task<DeleteDebtResponse> DeleteDebt(int? id)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var debt = await context
            .Debts
            .FirstOrDefaultAsync(x => x.Id == id);

        if (debt is null)
        {
            _logger.LogError("Debt(Id: {DebtId}) was not found", id);
            throw new NullReferenceException($"Debt({id}) was not found");
        }

        context.Debts.Remove(debt);
        context.SaveChanges();

        await _cacheService.ClearStorage();
        
        _logger.LogInformation("--> Debt(Id: {DebtId}) was successfully removed.", id);

        var response = _mapper.Map<DeleteDebtResponse>(debt);
        return response;
    }
}
