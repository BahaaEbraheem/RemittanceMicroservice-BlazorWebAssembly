using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Tasky.CustomerService.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
