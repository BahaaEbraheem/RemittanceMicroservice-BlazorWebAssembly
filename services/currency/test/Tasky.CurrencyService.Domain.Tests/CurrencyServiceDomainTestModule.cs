using Tasky.CurrencyService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tasky.CurrencyService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(CurrencyServiceEntityFrameworkCoreTestModule)
    )]
public class CurrencyServiceDomainTestModule : AbpModule
{

}
