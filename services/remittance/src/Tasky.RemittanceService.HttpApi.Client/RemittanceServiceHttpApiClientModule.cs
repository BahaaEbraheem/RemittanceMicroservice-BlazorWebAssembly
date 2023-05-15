using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Tasky.RemittanceService;

[DependsOn(
    typeof(RemittanceServiceApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class RemittanceServiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(RemittanceServiceApplicationContractsModule).Assembly,
            RemittanceServiceRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<RemittanceServiceHttpApiClientModule>();
        });

    }
}
