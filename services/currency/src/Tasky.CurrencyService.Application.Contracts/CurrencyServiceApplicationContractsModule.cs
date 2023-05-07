using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Tasky.CurrencyService;

[DependsOn(
    typeof(CurrencyServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class CurrencyServiceApplicationContractsModule : AbpModule
{

}
