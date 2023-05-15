﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Tasky.Microservice.Shared.Dtos;
using Volo.Abp.Validation;

namespace Tasky.RemittanceService.Remittances;

//[Area(RemittanceManagementRemoteServiceConsts.ModuleName)]
//[RemoteService(Name = RemittanceManagementRemoteServiceConsts.RemoteServiceName)]
//[Route("api/RemittanceManagement/remittance")]
[RemoteService]
[Area("remittanceService")]
[Route("api/remittanceService/remittance")]
public class RemittanceController : RemittanceServiceController, IRemittanceAppService
{
    private readonly IRemittanceAppService _remittanceAppService;

    public RemittanceController(IRemittanceAppService remittanceAppService)
    {
        _remittanceAppService = remittanceAppService;
    }
    [HttpPost]
    [Route("CreateAsync")]
    public async Task<RemittanceDto> CreateAsync(CreateRemittanceDto input)
    {
        return await _remittanceAppService.CreateAsync(input);
    }
    [HttpDelete]
    [Route("DeleteAsync")]
    public Task DeleteAsync(Guid id)
    {
        return _remittanceAppService.DeleteAsync(id);
    }

    [HttpGet]
    [Route("GetAsync")]
    public async Task<RemittanceDto> GetAsync(Guid id)
    {
        return await _remittanceAppService.GetAsync(id);
    }
    [HttpGet]
    [Route("GetCurrencyLookupAsync")]
    public async Task<ListResultDto<CurrencyLookupDto>> GetCurrencyLookupAsync()
    {
        return await _remittanceAppService.GetCurrencyLookupAsync();
    }
    [HttpGet]
    [Route("GetCustomerLookupAsync")]
    public async Task<ListResultDto<CustomerLookupDto>> GetCustomerLookupAsync()
    {
        return await _remittanceAppService.GetCustomerLookupAsync();
    }

    [HttpGet]
    [DisableValidation]
    [Route("GetList")]
    public virtual async Task<PagedResultDto<RemittanceDto>> GetListAsync(GetRemittanceListDto input)
    {
        return await _remittanceAppService.GetListAsync(input);
    }
    [HttpGet]
    [DisableValidation]
    [Route("GetListRemittancesForCreator")]
    public virtual async Task<PagedResultDto<RemittanceDto>> GetListRemittancesForCreator(GetRemittanceListPagedAndSortedResultRequestDto input)
    {
        return await _remittanceAppService.GetListRemittancesForCreator(input);
    }
    [HttpGet]
    [Route("GetListRemittancesForSupervisor")]
    public virtual async Task<PagedResultDto<RemittanceDto>> GetListRemittancesForSupervisor(GetRemittanceListPagedAndSortedResultRequestDto input)
    {
        return await _remittanceAppService.GetListRemittancesForSupervisor(input);

    }

    [HttpGet]
    [Route("GetListRemittancesForReleaser")]
    public async Task<PagedResultDto<RemittanceDto>> GetListRemittancesForReleaser(GetRemittanceListPagedAndSortedResultRequestDto input)
    {
        return await _remittanceAppService.GetListRemittancesForReleaser(input);
    }

    [HttpGet]
    [Route("GetListRemittancesStatus")]
    public async Task<PagedResultDto<RemittanceDto>> GetListRemittancesStatusAsync(GetRemittanceListPagedAndSortedResultRequestDto input)
    {
        return await _remittanceAppService.GetListRemittancesStatusAsync(input);
    }
    [HttpPost]
    [Route("SetApprove")]
    public async Task SetApprove(RemittanceDto input)
    {
        await _remittanceAppService.SetApprove(input);
    }
    [HttpPost]
    [Route("SetReady")]
    public async Task SetReady(RemittanceDto input)
    {
        await _remittanceAppService.SetReady(input);
    }
    [HttpPost]
    [Route("SetRelease")]
    public async Task SetRelease(RemittanceDto input)
    {
        await _remittanceAppService.SetRelease(input);
    }
    [HttpPut]
    [Route("UpdateAsync")]
    public async Task UpdateAsync(Guid id, UpdateRemittanceDto input)
    {
        await _remittanceAppService.UpdateAsync(id, input);
    }
    [HttpPost]
    [Route("SetAmlChecked")]
    public async Task SetAmlChecked(Guid? id)
    {
        await _remittanceAppService.SetAmlChecked(id);
    }

    [HttpGet]
    [Route("GetAllAsync")]
    public async Task<List<RemittanceDto>> GetAllAsync()
    {
        return await _remittanceAppService.GetAllAsync();

    }
}