using Volo.Abp.Modularity;

namespace Tasky.CustomerService;

[DependsOn(
    typeof(CustomerServiceApplicationModule),
    typeof(CustomerServiceDomainTestModule)
    )]
public class CustomerServiceApplicationTestModule : AbpModule
{

}
