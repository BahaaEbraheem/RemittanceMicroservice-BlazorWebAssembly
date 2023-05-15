using Tasky.RemittanceService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tasky.RemittanceService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(RemittanceServiceEntityFrameworkCoreTestModule)
    )]
public class RemittanceServiceDomainTestModule : AbpModule
{

}
