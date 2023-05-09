using Microsoft.Extensions.DependencyInjection;
using Tasky.CurrencyService.Currencies;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tasky.CurrencyService.EntityFrameworkCore;

[DependsOn(
    typeof(CurrencyServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class CurrencyServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<CurrencyServiceDbContext>(options =>
        {

            options.AddDefaultRepositories(true);
            options.AddRepository<Currency, EfCoreCurrencyRepository>();
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
        });
    }
}
