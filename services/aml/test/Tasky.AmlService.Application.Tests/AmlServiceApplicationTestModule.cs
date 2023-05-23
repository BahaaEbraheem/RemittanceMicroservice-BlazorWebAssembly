using Volo.Abp.Modularity;

namespace Tasky.AmlService;

[DependsOn(
    typeof(AmlServiceApplicationModule),
    typeof(AmlServiceDomainTestModule)
    )]
public class AmlServiceApplicationTestModule : AbpModule
{

}
