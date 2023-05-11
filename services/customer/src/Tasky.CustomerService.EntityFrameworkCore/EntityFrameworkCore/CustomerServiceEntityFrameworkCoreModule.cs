using Microsoft.Extensions.DependencyInjection;
using Tasky.CustomerService.Customers;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tasky.CustomerService.EntityFrameworkCore;

[DependsOn(
    typeof(CustomerServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class CustomerServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<CustomerServiceDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
            options.AddRepository<Customer, EfCoreCustomerRepository>();


        });

    }
}
