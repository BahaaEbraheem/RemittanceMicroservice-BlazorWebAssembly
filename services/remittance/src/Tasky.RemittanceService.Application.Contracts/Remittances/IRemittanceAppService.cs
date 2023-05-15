using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tasky.Microservice.Shared.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tasky.RemittanceService.Remittances
{
    public interface IRemittanceAppService : IApplicationService
    {
        Task<RemittanceDto> GetAsync(Guid id);
        Task<List<RemittanceDto>> GetAllAsync();

        Task<PagedResultDto<RemittanceDto>> GetListAsync(GetRemittanceListDto input);
        Task<PagedResultDto<RemittanceDto>> GetListRemittancesForCreator(GetRemittanceListPagedAndSortedResultRequestDto input);
        Task<PagedResultDto<RemittanceDto>> GetListRemittancesForSupervisor(GetRemittanceListPagedAndSortedResultRequestDto input);

        Task<PagedResultDto<RemittanceDto>> GetListRemittancesForReleaser(GetRemittanceListPagedAndSortedResultRequestDto input);

        Task<PagedResultDto<RemittanceDto>> GetListRemittancesStatusAsync(GetRemittanceListPagedAndSortedResultRequestDto input);


        Task<RemittanceDto> CreateAsync(CreateRemittanceDto input);

        Task UpdateAsync(Guid id, UpdateRemittanceDto input);

        Task DeleteAsync(Guid id);

        Task<ListResultDto<CurrencyLookupDto>> GetCurrencyLookupAsync();

        Task<ListResultDto<CustomerLookupDto>> GetCustomerLookupAsync();

        //Task<ListResultDto<UserLookupDto>> GetUserLookupAsync();
        Task SetReady(RemittanceDto input);
        Task SetApprove(RemittanceDto input);
        Task SetRelease(RemittanceDto input);
        Task SetAmlChecked(Guid? id);


    }
}