using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Tasky.RemittanceService;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(RemittanceServiceHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class RemittanceServiceConsoleApiClientModule : AbpModule
{

}
