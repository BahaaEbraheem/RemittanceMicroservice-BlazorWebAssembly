using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Tasky.currencyService;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(currencyServiceHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class currencyServiceConsoleApiClientModule : AbpModule
{

}
