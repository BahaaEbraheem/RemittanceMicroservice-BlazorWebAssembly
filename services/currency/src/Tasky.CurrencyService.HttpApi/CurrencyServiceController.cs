using Tasky.CurrencyService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tasky.CurrencyService;

public abstract class CurrencyServiceController : AbpControllerBase
{
    protected CurrencyServiceController()
    {
        LocalizationResource = typeof(CurrencyServiceResource);
    }
}
