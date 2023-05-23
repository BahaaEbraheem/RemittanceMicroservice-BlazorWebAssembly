using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Tasky.AmlService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AmlServiceDomainSharedModule)
)]
public class AmlServiceDomainModule : AbpModule
{

}
