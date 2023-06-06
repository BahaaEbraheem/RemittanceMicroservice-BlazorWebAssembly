using Microsoft.Extensions.DependencyInjection;
using Tasky.AmlService.Aml_Person;
using Tasky.AmlService.Aml_Remittance;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tasky.AmlService.EntityFrameworkCore;

[DependsOn(
    typeof(AmlServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class AmlServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<AmlServiceDbContext>(options =>
        {
            options.AddRepository<AmlPerson, AmlPersonRepository>();
            options.AddRepository<AmlRemittance, AmlRemittanceRepository>();
            options.AddDefaultRepositories();
        });
    }
}
