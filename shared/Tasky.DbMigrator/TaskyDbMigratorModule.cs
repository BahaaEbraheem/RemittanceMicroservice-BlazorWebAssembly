using Tasky.Administration;
using Tasky.Administration.EntityFrameworkCore;
using Tasky.AmlService;
using Tasky.AmlService.EntityFrameworkCore;
using Tasky.CurrencyService;
using Tasky.CurrencyService.EntityFrameworkCore;
using Tasky.CustomerService;
using Tasky.CustomerService.EntityFrameworkCore;
using Tasky.IdentityService;
using Tasky.IdentityService.EntityFrameworkCore;
using Tasky.Microservice.Shared;
using Tasky.RemittanceService;
using Tasky.RemittanceService.EntityFrameworkCore;
using Tasky.SaaS;
using Tasky.SaaS.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Tasky.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AdministrationEntityFrameworkCoreModule),
    typeof(AdministrationApplicationContractsModule),
    typeof(IdentityServiceEntityFrameworkCoreModule),
    typeof(IdentityServiceApplicationContractsModule),
    typeof(SaaSEntityFrameworkCoreModule),
    typeof(SaaSApplicationContractsModule),
    typeof(CurrencyServiceEntityFrameworkCoreModule),
    typeof(CurrencyServiceApplicationContractsModule),
    typeof(CustomerServiceEntityFrameworkCoreModule),
    typeof(CustomerServiceApplicationContractsModule),
    typeof(RemittanceServiceEntityFrameworkCoreModule),
    typeof(RemittanceServiceApplicationContractsModule),
    typeof(AmlServiceEntityFrameworkCoreModule),
    typeof(AmlServiceApplicationContractsModule)
)]
public class TaskyDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}