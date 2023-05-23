using Tasky.AmlService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tasky.AmlService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(AmlServiceEntityFrameworkCoreTestModule)
    )]
public class AmlServiceDomainTestModule : AbpModule
{

}
