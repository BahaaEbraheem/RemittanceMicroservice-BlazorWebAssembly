using Microsoft.Extensions.DependencyInjection;
using Tasky.AmlService;
using Tasky.CurrencyService;
using Tasky.CustomerService;
using Tasky.RemittanceService;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace Tasky.Administration;

[DependsOn(
    typeof(AdministrationDomainModule),
    typeof(AdministrationApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
   typeof(CurrencyServiceHttpApiClientModule),
   typeof(CustomerServiceHttpApiClientModule),
   typeof(RemittanceServiceHttpApiClientModule),
   typeof(AmlServiceHttpApiClientModule)

)]
[DependsOn(typeof(AbpPermissionManagementApplicationModule))]
[DependsOn(typeof(AbpSettingManagementApplicationModule))]
[DependsOn(typeof(AbpFeatureManagementApplicationModule))]
public class AdministrationApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AdministrationApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AdministrationApplicationModule>(true);
        });
    }
}