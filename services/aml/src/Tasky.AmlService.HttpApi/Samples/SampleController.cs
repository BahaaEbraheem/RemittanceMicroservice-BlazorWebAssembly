﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasky.AmlService.Aml_Remittance;
using Tasky.Microservice.Shared.Dtos;
using Tasky.Microservice.Shared.Etos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Tasky.AmlService.Samples;

    [Area(AmlServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = AmlServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/amlService/sample")]
    public class SampleController : AmlServiceController, ISampleAppService
    {
        private readonly ISampleAppService _sampleAppService;

        public SampleController(ISampleAppService sampleAppService)
        {
            _sampleAppService = sampleAppService;
        }


    [HttpGet]
    [DisableValidation]
    [Route("GetListRemittancesForAmlCheckerAsync")]
    public virtual async Task<PagedResultDto<RemittanceEto>> GetListRemittancesForAmlCheckerAsync(GetRemittanceListPagedAndSortedResultRequestDto input)
    {
        return await _sampleAppService.GetListRemittancesForAmlCheckerAsync(input);

    }

    [HttpGet]
    [Route("CheckAML")]
    public async Task CheckAML(Guid id)
    {
        await _sampleAppService.CheckAML(id);
    }

    [HttpGet]
    public async Task<SampleDto> GetAsync()
    {
        return await _sampleAppService.GetAsync();
    }

    [HttpGet]
    [Route("authorized")]
    [Authorize]
    public async Task<SampleDto> GetAuthorizedAsync()
    {
        return await _sampleAppService.GetAsync();
    }


}
