using Volo.Abp.Modularity;

namespace Tasky.CurrencyService;

[DependsOn(
    typeof(CurrencyServiceApplicationModule),
    typeof(CurrencyServiceDomainTestModule)
    )]
public class CurrencyServiceApplicationTestModule : AbpModule
{

}
