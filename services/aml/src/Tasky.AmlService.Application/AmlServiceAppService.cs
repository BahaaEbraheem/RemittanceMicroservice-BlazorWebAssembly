using Tasky.AmlService.Localization;
using Volo.Abp.Application.Services;

namespace Tasky.AmlService;

public abstract class AmlServiceAppService : ApplicationService
{
    protected AmlServiceAppService()
    {
        LocalizationResource = typeof(AmlServiceResource);
        ObjectMapperContext = typeof(AmlServiceApplicationModule);
    }
}
