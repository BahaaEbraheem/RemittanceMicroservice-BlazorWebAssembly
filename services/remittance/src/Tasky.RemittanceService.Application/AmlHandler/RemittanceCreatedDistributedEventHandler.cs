﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Tasky.Microservice.Shared.Etos;
using Tasky.RemittanceService.Remittances;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectMapping;
using IObjectMapper = Volo.Abp.ObjectMapping.IObjectMapper;

namespace Tasky.RemittanceService.AmlHandler
{
    public class RemittanceAfterCheckAmlDistributedEventHandler : IDistributedEventHandler<RemittanceAfterCheckedAmlEto>, ITransientDependency
    {
        private readonly IObjectMapper _ObjectMapper;
        private readonly IRemittanceAppService _remittanceAppService;

        public RemittanceAfterCheckAmlDistributedEventHandler(
            IObjectMapper ObjectMapper,
            IRemittanceAppService remittanceAppService)
        {
            _ObjectMapper = ObjectMapper;
            _remittanceAppService = remittanceAppService;
        }

        public async Task HandleEventAsync(RemittanceAfterCheckedAmlEto eventData)
        {
            await _remittanceAppService.SetAmlChecked(eventData.RemittanceId);

        }
    }
}