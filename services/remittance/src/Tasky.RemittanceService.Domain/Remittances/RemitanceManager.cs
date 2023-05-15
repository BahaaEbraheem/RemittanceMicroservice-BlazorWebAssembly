﻿using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Services;
//using Microsoft.AspNetCore.Authorization;
//using RemittanceService.Currencies;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Volo.Abp.Validation;
//using Microsoft.AspNetCore.Identity;
//using Volo.Abp.Identity;
//using IdentityUser = Volo.Abp.Identity.IdentityUser;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;
using static Tasky.Microservice.Shared.Enums.Enums;

namespace Tasky.RemittanceService.Remittances
{
    public class RemittanceManager : DomainService
    {

        private readonly IRemittanceRepository _remittanceRepository;
        private readonly ICurrentUser _currentUser;



        public RemittanceManager(IRemittanceRepository remittanceRepository, ICurrentUser currentUser)
        {
            _remittanceRepository = remittanceRepository;
            _currentUser = currentUser;
        }



        public async Task<Remittance> CreateAsync(double amount, RemittanceType type,
            string receiverFullName, DateTime CreationTime, Guid currencyId, Guid senderBy)
        {
            Check.NotNullOrWhiteSpace(amount.ToString(), nameof(amount));
            Check.NotNullOrWhiteSpace(receiverFullName, nameof(receiverFullName));

            if (string.IsNullOrEmpty(type.ToString()) || type == 0)
            {
                type = RemittanceType.Internal;
            }
            //هل تم انشاء حوالة مسودة من قبل
            var existingDraftRemittance = await _remittanceRepository.FindRemittance_StillDraftAsync(amount, receiverFullName);
            if (existingDraftRemittance != null)
            {
                throw new ArgumentException(receiverFullName, "The Remittance Alredy Exist");
            }
            return new Remittance(
                 GuidGenerator.Create(),
                 amount,
                 _currentUser.Id,
                 type,
                receiverFullName,
                currencyId, senderBy
            );


        }
        public async Task UpdateAsync(Remittance remittance)
        {
            await _remittanceRepository.UpdateAsync(remittance);
        }
        public async Task<Remittance> GetAsync(Guid id)
        {
            return await _remittanceRepository.GetAsync(id);
        }

        public async Task<Remittance> UpdateAsync(Remittance remittance, double amount, RemittanceType type,
            string receiverFullName, [NotNull] Guid currencyId)
        {
            // هل الحوالة تمت الموافقة عليها أم لا
            bool approvedRemittance = await _remittanceRepository.IsApprovedRemittanceAsync(remittance);

            if (!approvedRemittance)
            {
                throw new RemittanceAlreadyApprovedException();
            }
            Check.NotNullOrWhiteSpace(amount.ToString(), nameof(amount));
            Check.NotNullOrWhiteSpace(receiverFullName, nameof(receiverFullName));
            Check.NotNullOrWhiteSpace(currencyId.ToString(), nameof(currencyId));

            return remittance;
        }


    }
}