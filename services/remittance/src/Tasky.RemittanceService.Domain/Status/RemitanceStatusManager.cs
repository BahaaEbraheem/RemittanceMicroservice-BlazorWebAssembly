﻿using JetBrains.Annotations;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Users;
using static Tasky.Microservice.Shared.Enums.Enums;

namespace Tasky.RemittanceService.Status
{
    public class RemitanceStatusManager : DomainService
    {
        private readonly IRemittanceStatusRepository _remittanceStatusRepository;

        public RemitanceStatusManager(IRemittanceStatusRepository remittanceStatusRepository)
        {
            _remittanceStatusRepository = remittanceStatusRepository;
        }

        public async Task<RemittanceStatus> CreateAsync(Guid remittanceId, Remittance_Status State)
        {

            return await Task.FromResult(new RemittanceStatus(
                 GuidGenerator.Create(),
                 remittanceId, State

            ));
        }

        public async Task InsertAsync(RemittanceStatus remittanceStatus)
        {
            await _remittanceStatusRepository.InsertAsync(remittanceStatus);
        }

        //public override Task InsertAsync(RemittanceStatus remittanceStatus)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<RemittanceStatus> UpdateAsync(Guid remittanceId)
        {
            Check.NotNullOrWhiteSpace(remittanceId.ToString(), nameof(remittanceId));
            //return last record to this remittance from remittanceStatus
            var LastRemitanceStatusCreation = await _remittanceStatusRepository.FindLastStateToThisRemitanceAsync(remittanceId);
            if (LastRemitanceStatusCreation == null)
            {
                throw new ArgumentNullException();
            }
            return new RemittanceStatus(
                 GuidGenerator.Create(),
                 remittanceId,
                 LastRemitanceStatusCreation.State
            );

        }

    }
}