using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Tasky.CustomerService;

[DependsOn(
    typeof(CustomerServiceApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class CustomerServiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(CustomerServiceApplicationContractsModule).Assembly,
            CustomerServiceRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CustomerServiceHttpApiClientModule>();
        });

    }
}
