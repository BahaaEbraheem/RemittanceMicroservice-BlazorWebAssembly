using Tasky.RemittanceService.Localization;
using Volo.Abp.Application.Services;

namespace Tasky.RemittanceService;

public abstract class RemittanceServiceAppService : ApplicationService
{
    protected RemittanceServiceAppService()
    {
        LocalizationResource = typeof(RemittanceServiceResource);
        ObjectMapperContext = typeof(RemittanceServiceApplicationModule);
    }
}
