using Tasky.CustomerService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tasky.CustomerService;

public abstract class CustomerServiceController : AbpControllerBase
{
    protected CustomerServiceController()
    {
        LocalizationResource = typeof(CustomerServiceResource);
    }
}
