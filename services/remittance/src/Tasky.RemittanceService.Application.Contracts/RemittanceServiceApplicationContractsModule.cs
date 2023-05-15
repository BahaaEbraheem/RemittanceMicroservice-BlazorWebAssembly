using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Tasky.CurrencyService;
using Tasky.CustomerService;

namespace Tasky.RemittanceService;

[DependsOn(
    typeof(RemittanceServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
     typeof(CurrencyServiceApplicationContractsModule),
    typeof(CustomerServiceApplicationContractsModule)
    )]
public class RemittanceServiceApplicationContractsModule : AbpModule
{

}
