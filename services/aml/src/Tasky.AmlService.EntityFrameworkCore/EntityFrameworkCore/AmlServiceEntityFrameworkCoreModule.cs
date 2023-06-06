using Microsoft.Extensions.DependencyInjection;
using Tasky.AmlService.Aml_Person;
using Tasky.AmlService.Aml_Remittance;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DistributedEvents;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;

namespace Tasky.AmlService.EntityFrameworkCore;

[DependsOn(
    typeof(AmlServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpEventBusRabbitMqModule)

)]
public class AmlServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDistributedEventBusOptions>(options =>
        {
            options.Outboxes.Configure(config =>
            {
                config.UseDbContext<AmlServiceDbContext>();
            });
            options.Inboxes.Configure(config =>
            {
                config.UseDbContext<AmlServiceDbContext>();
            });
        });
        context.Services.AddAbpDbContext<AmlServiceDbContext>(options =>
        {
            options.AddRepository<AmlPerson, AmlPersonRepository>();
            options.AddRepository<AmlRemittance, AmlRemittanceRepository>();
            options.AddDefaultRepositories();
        });
    }
}
