using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Tasky.RemittanceService;

namespace Tasky.AmlService;

[DependsOn(
    typeof(AmlServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(RemittanceServiceApplicationContractsModule)
    )]
public class AmlServiceApplicationContractsModule : AbpModule
{

}
