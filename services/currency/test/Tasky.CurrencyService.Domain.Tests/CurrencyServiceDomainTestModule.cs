using Tasky.currencyService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tasky.currencyService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(currencyServiceEntityFrameworkCoreTestModule)
    )]
public class currencyServiceDomainTestModule : AbpModule
{

}
