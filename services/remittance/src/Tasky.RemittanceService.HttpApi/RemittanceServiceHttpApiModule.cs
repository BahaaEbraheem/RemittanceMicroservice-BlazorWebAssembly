using Localization.Resources.AbpUi;
using Tasky.RemittanceService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Tasky.RemittanceService;

[DependsOn(
    typeof(RemittanceServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class RemittanceServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(RemittanceServiceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<RemittanceServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
