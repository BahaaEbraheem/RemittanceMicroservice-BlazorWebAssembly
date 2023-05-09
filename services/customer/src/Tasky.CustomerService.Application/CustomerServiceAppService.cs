using Tasky.CustomerService.Localization;
using Volo.Abp.Application.Services;

namespace Tasky.CustomerService;

public abstract class CustomerServiceAppService : ApplicationService
{
    protected CustomerServiceAppService()
    {
        LocalizationResource = typeof(CustomerServiceResource);
        ObjectMapperContext = typeof(CustomerServiceApplicationModule);
    }
}
