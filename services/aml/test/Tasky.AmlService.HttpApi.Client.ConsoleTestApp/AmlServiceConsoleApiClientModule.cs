using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Tasky.AmlService;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AmlServiceHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class AmlServiceConsoleApiClientModule : AbpModule
{

}
