using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Tasky.CurrencyService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(CurrencyServiceDomainSharedModule)
)]
public class CurrencyServiceDomainModule : AbpModule
{

}
