using Microsoft.Extensions.DependencyInjection;
using Tasky.RemittanceService.Remittances;
using Tasky.RemittanceService.Status;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tasky.RemittanceService.EntityFrameworkCore;

[DependsOn(
    typeof(RemittanceServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class RemittanceServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<RemittanceServiceDbContext>(options =>
        {
            options.AddRepository<Remittance, EfCoreRemittanceRepository>();
            options.AddRepository<RemittanceStatus, EfCoreRemittanceStatusRepository>();
            options.AddDefaultRepositories();
        });
    }
}
