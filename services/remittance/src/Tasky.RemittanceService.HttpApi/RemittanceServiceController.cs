using Tasky.RemittanceService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tasky.RemittanceService;

public abstract class RemittanceServiceController : AbpControllerBase
{
    protected RemittanceServiceController()
    {
        LocalizationResource = typeof(RemittanceServiceResource);
    }
}
