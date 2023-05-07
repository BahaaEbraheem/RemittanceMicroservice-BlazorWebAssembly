using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Tasky.CurrencyService.Currencies
{
    public class CurrencyManager : DomainService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyManager(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }


        public async Task IsCurrencyUsedBeforInRemittance(Guid id)
        {
            Check.NotNull(id, nameof(id));
            //var x = new GetRemittanceListDto();
            //var remittancequeryable = await _remittanceRepository.GetListAsync();
            //var remittance = remittancequeryable.Where(a => a.CurrencyId == id).FirstOrDefault();
            //if (remittance != null)
            //{
            //    string remittanceSerial = remittance.SerialNumber;
            //    throw new CurrencyAlreadyUsedInRemittanceException(remittanceSerial);
            //}
            await Task.CompletedTask;
        }
    }
}
