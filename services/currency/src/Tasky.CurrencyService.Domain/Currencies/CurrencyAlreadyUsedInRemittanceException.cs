using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasky.CurrencyService;
using Volo.Abp;

namespace Tasky.CurrencyService.Currencies
{
    public class CurrencyAlreadyUsedInRemittanceException : BusinessException
    {
        public CurrencyAlreadyUsedInRemittanceException(string remittanceSerial)
            : base(CurrencyServiceErrorCodes.CurrencyAlreadyUsedInRemittance)
        {
            WithData("remittanceSerial", remittanceSerial);
        }
    }

}
