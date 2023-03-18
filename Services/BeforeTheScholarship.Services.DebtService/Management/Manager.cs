using AutoMapper;
using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Common.Validation;
using BeforeTheScholarship.Context;
using BeforeTheScholarship.Entities;
using BeforeTheScholarship.Services.Actions;
using BeforeTheScholarship.Services.CacheService;
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

    protected async Task<IEnumerable<DebtResponse>> GetDebtsResponse(Guid? studentId = null)
    {
        using var context = await _dbContext.CreateDbContextAsync();
        var debt = context
            .Debts
            .AsQueryable();

        var response = (studentId is null)
            ? (await debt.ToListAsync()).Select(s => _mapper.Map<DebtResponse>(s))
            : (await debt.ToListAsync()).Where(x => x.StudentId == studentId).Select(s => _mapper.Map<DebtResponse>(s))
            ?? new List<DebtResponse>();

        return response;
    }

    protected async Task<IEnumerable<DebtResponse>?> ReturnCachedDebts(string key)
    {
        var cachedData = await _cacheService.GetStringAsync<IEnumerable<DebtResponse>>(key);

        return cachedData;
    }

    protected async Task CreateDebtSendEmailAction(Debts data)
    {
        var delay = (data.WhenToPayback - data.WhenToPayback.AddDays(-1)).TotalMilliseconds;

        var student = await _studentService.GetStudentById(data.StudentId);

        var content = PathReader.ReadContent(
                                        Directory.GetCurrentDirectory() + "\\EmailPages\\debtNotification.html",
                                        "/app/emailpages/debtNotification.html")
                                        .Replace("DATETIMENOW", $"{DateTimeOffset.Now.DateTime.ToShortDateString()}")
                                        .Replace("STUDENTNAME", $"{student.UserName}")
                                        .Replace("BORROWED", $"{data.Borrowed}")
                                        .Replace("WHENTOPAYBACK", $"{data.WhenToPayback.DateTime.ToShortDateString()}");

        await _actionService.SendDebtEmail(new DebtEmailModel()
        {
            EmailTo = student.Email,
            Subject = "One of your debts is about to expire",
            Message = content,
            WhenToPayback = data.WhenToPayback
        }, 30000);
    }
}
