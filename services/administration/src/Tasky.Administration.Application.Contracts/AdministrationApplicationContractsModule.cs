using Tasky.AmlService;
using Tasky.CurrencyService;
using Tasky.CustomerService;
using Tasky.RemittanceService;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace Tasky.Administration;

[DependsOn(
    typeof(AdministrationDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(CurrencyServiceApplicationContractsModule),
    typeof(CustomerServiceApplicationContractsModule),
    typeof(RemittanceServiceApplicationContractsModule),
    typeof(AmlServiceApplicationContractsModule)

)]
[DependsOn(typeof(AbpPermissionManagementApplicationContractsModule))]
[DependsOn(typeof(AbpSettingManagementApplicationContractsModule))]
[DependsOn(typeof(AbpFeatureManagementApplicationContractsModule))]
public class AdministrationApplicationContractsModule : AbpModule
{
}