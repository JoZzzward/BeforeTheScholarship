﻿using AutoMapper;
using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Context;
using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Services.Actions;
using BeforeTheScholarship.Services.CacheService;
using BeforeTheScholarship.Services.DebtService.Helpers;
using BeforeTheScholarship.Services.EmailSender;
using BeforeTheScholarship.Services.StudentService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BeforeTheScholarship.Services.DebtService;

public abstract class Manager
{
    private readonly IDbContextFactory<AppDbContext> _dbContext;
    private readonly IStudentService _studentService;
    private readonly ILogger<DebtService> _logger;
    private readonly IMapper _mapper;
    private readonly IActionsService _actionService;
    private readonly ICacheService _cacheService;

    public Manager(
        IDbContextFactory<AppDbContext> dbContext,
        IStudentService studentService,
        ILogger<DebtService> logger,
        IMapper mapper,
        IActionsService actionService,
        ICacheService cacheService)
    {
        _dbContext = dbContext;
        _studentService = studentService;
        _logger = logger;
        _mapper = mapper;
        _actionService = actionService;
        _cacheService = cacheService;
    }

    protected async Task<IEnumerable<DebtResponse>?> GetDebtsResponse(Guid? debtId = null)
    {
        using var context = await _dbContext.CreateDbContextAsync();
        var debt = context
            .Debts
            .AsQueryable();

        var response = (debtId is null)
            ? (await debt.ToListAsync()).Select(_mapper.Map<DebtResponse>)
            : (await debt.ToListAsync()).Where(x => x.StudentId == debtId).Select(_mapper.Map<DebtResponse>);

        if (!response.Any())
            return null;

        return response;
    }

    protected async Task<IEnumerable<DebtResponse>?> ReturnCachedDebts(string key)
    {
        var cachedData = await _cacheService.GetStringAsync<IEnumerable<DebtResponse>>(key);

        return cachedData;
    }

    protected async Task CreateSendDebtEmailAction(Debts data)
    {
        // TODO: Put delay time in configuration file
        var delay = (data.WhenToPayback - data.WhenToPayback.AddDays(-1)).TotalMilliseconds;

        var debt = await _studentService.GetStudentById(data.StudentId);

        var content = ContentReader.ReadFromFile("debtNotification.html", debt.UserName, data);

        await _actionService.SendDebtEmail(new DebtEmailModel()
        {
            EmailTo = debt.Email,
            Subject = "One of your debts is about to expire",
            Message = content,
            WhenToPayback = data.WhenToPayback
        }, delay);

        _logger.LogInformation("--> Notification action for debt(Id: {StudentId}) created successfully", debt.Id);
    }
}
