using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Tasky.CurrencyService;

[DependsOn(
    typeof(CurrencyServiceApplicationContractsModule),
    typeof(AbpHttpClientModule))]
//[DependsOn(typeof(AbpPermissionManagementHttpApiClientModule))]
//[DependsOn(typeof(AbpSettingManagementHttpApiClientModule))]
//[DependsOn(typeof(AbpFeatureManagementHttpApiClientModule))]
//[DependsOn(typeof(AbpIdentityHttpApiClientModule))]
//[DependsOn(typeof(AbpAccountHttpApiClientModule))]
public class CurrencyServiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(CurrencyServiceApplicationContractsModule).Assembly,
            CurrencyServiceRemoteServiceConsts.RemoteServiceName
        );
        //        context.Services.AddStaticHttpClientProxies(
        //    typeof(CurrencyServiceHttpApiClientModule).Assembly
        //);
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CurrencyServiceHttpApiClientModule>();
        });

    }
}
