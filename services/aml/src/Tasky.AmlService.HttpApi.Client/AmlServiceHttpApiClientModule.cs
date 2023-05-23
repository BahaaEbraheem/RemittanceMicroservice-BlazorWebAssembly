using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Tasky.AmlService;

[DependsOn(
    typeof(AmlServiceApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class AmlServiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(AmlServiceApplicationContractsModule).Assembly,
            AmlServiceRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AmlServiceHttpApiClientModule>();
        });

    }
}
