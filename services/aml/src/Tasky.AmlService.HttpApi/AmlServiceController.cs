using Tasky.AmlService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tasky.AmlService;

public abstract class AmlServiceController : AbpControllerBase
{
    protected AmlServiceController()
    {
        LocalizationResource = typeof(AmlServiceResource);
    }
}
