using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Tasky.RemittanceService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(RemittanceServiceDomainSharedModule)
)]
public class RemittanceServiceDomainModule : AbpModule
{

}
