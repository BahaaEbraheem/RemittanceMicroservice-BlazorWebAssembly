using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Tasky.RemittanceService;

namespace Tasky.CurrencyService;

[DependsOn(
    typeof(CurrencyServiceDomainModule),
    typeof(CurrencyServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(RemittanceServiceHttpApiClientModule)
    )]
public class CurrencyServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<CurrencyServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<CurrencyServiceApplicationModule>(validate: true);
        });
    }
}
