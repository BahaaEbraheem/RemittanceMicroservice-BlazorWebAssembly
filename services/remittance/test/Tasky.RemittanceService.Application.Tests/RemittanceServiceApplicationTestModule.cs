using Volo.Abp.Modularity;

namespace Tasky.RemittanceService;

[DependsOn(
    typeof(RemittanceServiceApplicationModule),
    typeof(RemittanceServiceDomainTestModule)
    )]
public class RemittanceServiceApplicationTestModule : AbpModule
{

}
