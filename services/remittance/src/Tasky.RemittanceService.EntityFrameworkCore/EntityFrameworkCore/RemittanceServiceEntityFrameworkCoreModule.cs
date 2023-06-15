using Microsoft.Extensions.DependencyInjection;
using Tasky.RemittanceService.Remittances;
using Tasky.RemittanceService.Status;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DistributedEvents;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;

namespace Tasky.RemittanceService.EntityFrameworkCore;

[DependsOn(
    typeof(RemittanceServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AbpEventBusRabbitMqModule)

)]
public class RemittanceServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDistributedEventBusOptions>(options =>
        {
            options.Outboxes.Configure("Default", config =>
            {
                config.UseDbContext<RemittanceServiceDbContext>();
                config.IsSendingEnabled = true;
                config.ImplementationType=config.ImplementationType;
                config.Selector=config.Selector;
            });
          
        });
        Configure<AbpDistributedEventBusOptions>(options =>
        {
          
            options.Outboxes.Configure(config =>
            {
                config.UseDbContext<RemittanceServiceDbContext>();
            });
            options.Inboxes.Configure(config =>
            {
                config.UseDbContext<RemittanceServiceDbContext>();
            });
        });
        context.Services.AddAbpDbContext<RemittanceServiceDbContext>(options =>
        {
            options.AddRepository<Remittance, EfCoreRemittanceRepository>();
            options.AddRepository<RemittanceStatus, EfCoreRemittanceStatusRepository>();
            options.AddDefaultRepositories();
        });
    }
}
