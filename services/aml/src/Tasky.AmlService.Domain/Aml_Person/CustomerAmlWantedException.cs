using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Tasky.AmlService.Aml_Person
{
    public class CustomerAmlWantedException : BusinessException
    {
        public CustomerAmlWantedException(string customerName)
            : base(AmlServiceErrorCodes.CustomerAmlWantedException)
        {
            WithData("customerName", customerName);
        }

    }
}