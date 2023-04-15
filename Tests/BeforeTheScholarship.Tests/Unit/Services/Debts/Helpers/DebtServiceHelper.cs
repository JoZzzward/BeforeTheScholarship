using BeforeTheScholarship.Services.Actions;
using BeforeTheScholarship.Services.DebtService;
using BeforeTheScholarship.Tests.Unit.Helpers.Configuration;
using BeforeTheScholarship.Tests.Unit.Helpers.Data;
using BeforeTheScholarship.Tests.Unit.Services.Debts.Helpers.ValidationHelpers;
using BeforeTheScholarship.Tests.Unit.Services.Students.Helpers;

namespace BeforeTheScholarship.Tests.Unit.Services.Debts.Helpers;

internal class DebtServiceHelper
{
    private readonly ValidationSetup _validationSetup = new();
    private readonly StudentServiceHelper _studentServiceHelper = new();

    public DebtService CreateDebtService()
    {
        var debtService = new DebtService(
            new AppDbContextFactory(),
            _studentServiceHelper.InitializeStudentService(),
            LoggerInitializer.InitializeForType<DebtService>(),
            AutoMapperInitializer.Initialize(),
            Substitute.For<IActionsService>(),
            CacheServiceInitializer.Initialize(),
            _validationSetup.SetupCreateDebtModelValidatorReturnsTrue(),
            _validationSetup.SetupUpdateDebtModelValidatorReturnsTrue());

        return debtService;
    }
}