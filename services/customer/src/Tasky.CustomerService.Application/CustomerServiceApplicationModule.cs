using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Tasky.RemittanceService;

namespace Tasky.CustomerService;

[DependsOn(
    typeof(CustomerServiceDomainModule),
    typeof(CustomerServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(RemittanceServiceHttpApiClientModule)
    )]
public class CustomerServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<CustomerServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<CustomerServiceApplicationModule>(validate: true);
        });
    }
}
