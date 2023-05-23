using Localization.Resources.AbpUi;
using Tasky.AmlService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Tasky.AmlService;

[DependsOn(
    typeof(AmlServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class AmlServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AmlServiceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AmlServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
