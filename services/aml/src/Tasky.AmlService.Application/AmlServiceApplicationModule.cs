using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.EventBus.RabbitMq;

namespace Tasky.AmlService;

[DependsOn(
    typeof(AmlServiceDomainModule),
    typeof(AmlServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule),
     typeof(AbpEventBusRabbitMqModule),
    typeof(AbpAutoMapperModule)
    )]
public class AmlServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AmlServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AmlServiceApplicationModule>(validate: true);
        });
    }
}
