using System;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Tasky.CurrencyService;

[DependsOn(
    typeof(CurrencyServiceApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class CurrencyServiceHttpApiClientModule : AbpModule
{
    //public override void PreConfigureServices(ServiceConfigurationContext context)
    //{
    //    PreConfigure<AbpHttpClientBuilderOptions>(options =>
    //    {
    //        options.ProxyClientBuildActions.Add((remoteServiceName, clientBuilder) =>
    //        {
    //            clientBuilder.AddTransientHttpErrorPolicy(policyBuilder =>
    //                policyBuilder.WaitAndRetryAsync(
    //                    3,
    //                    i => TimeSpan.FromSeconds(Math.Pow(2, i))
    //                )
    //            );
    //        });
    //    });
    //}
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(CurrencyServiceApplicationContractsModule).Assembly,
            CurrencyServiceRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CurrencyServiceHttpApiClientModule>();
        });

    }
}
