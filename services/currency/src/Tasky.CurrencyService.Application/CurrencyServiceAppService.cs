using Tasky.CurrencyService.Localization;
using Volo.Abp.Application.Services;

namespace Tasky.CurrencyService;

public abstract class CurrencyServiceAppService : ApplicationService
{
    protected CurrencyServiceAppService()
    {
        LocalizationResource = typeof(CurrencyServiceResource);
        ObjectMapperContext = typeof(CurrencyServiceApplicationModule);
    }
}
