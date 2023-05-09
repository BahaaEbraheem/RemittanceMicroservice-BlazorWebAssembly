using Volo.Abp.Modularity;

namespace Tasky.currencyService;

[DependsOn(
    typeof(currencyServiceApplicationModule),
    typeof(currencyServiceDomainTestModule)
    )]
public class currencyServiceApplicationTestModule : AbpModule
{

}
