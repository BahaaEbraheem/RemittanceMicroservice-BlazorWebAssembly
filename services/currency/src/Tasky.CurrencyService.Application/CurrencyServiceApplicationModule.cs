using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Tasky.RemittanceService;
using Volo.Abp.Http.Client;
using Polly;
using System;

namespace Tasky.CurrencyService;

[DependsOn(
    typeof(CurrencyServiceDomainModule),
    typeof(CurrencyServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(RemittanceServiceHttpApiClientModule)
    )]
public class CurrencyServiceApplicationModule : AbpModule
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
        context.Services.AddAutoMapperObjectMapper<CurrencyServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<CurrencyServiceApplicationModule>(validate: true);
        });
    }
}
