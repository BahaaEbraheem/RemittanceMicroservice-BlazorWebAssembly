using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasky.AmlService.Aml_Remittance;
using Tasky.Microservice.Shared.Dtos;
using Tasky.Microservice.Shared.Etos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Tasky.AmlService.Samples;

public interface ISampleAppService :  IApplicationService,ITransientDependency
{
    Task<PagedResultDto<RemittanceEto>> GetListRemittancesForAmlCheckerAsync(GetRemittanceListPagedAndSortedResultRequestDto input);
    Task CheckAML(Guid id);
    Task<SampleDto> GetAsync();
    Task<SampleDto> GetAuthorizedAsync();
}

