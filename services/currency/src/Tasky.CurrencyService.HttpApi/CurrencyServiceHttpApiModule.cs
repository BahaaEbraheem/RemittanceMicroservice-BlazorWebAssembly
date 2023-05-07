using Localization.Resources.AbpUi;
using Tasky.CurrencyService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Tasky.CurrencyService;

[DependsOn(
    typeof(CurrencyServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class CurrencyServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CurrencyServiceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<CurrencyServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
