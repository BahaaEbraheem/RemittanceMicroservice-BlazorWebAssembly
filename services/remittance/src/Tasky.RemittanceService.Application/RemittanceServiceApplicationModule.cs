using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Tasky.CurrencyService;
using Tasky.CustomerService;
using Volo.Abp.EventBus.RabbitMq;

namespace Tasky.RemittanceService;

[DependsOn(
    typeof(RemittanceServiceDomainModule),
    typeof(RemittanceServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(CurrencyServiceHttpApiClientModule),
    typeof(CustomerServiceHttpApiClientModule),
    typeof(AbpEventBusRabbitMqModule)
    )]
public class RemittanceServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<RemittanceServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<RemittanceServiceApplicationModule>(validate: true);
        });
    }
}
