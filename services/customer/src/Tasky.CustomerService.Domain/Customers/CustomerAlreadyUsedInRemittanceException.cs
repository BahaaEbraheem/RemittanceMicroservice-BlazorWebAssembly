using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Tasky.CustomerService.Customers
{
    public class CustomerAlreadyUsedInRemittanceException : BusinessException
    {
        public CustomerAlreadyUsedInRemittanceException(string customerName)
            : base(CustomerServiceErrorCodes.CustomerAlreadyUsedInRemittance)
        {
            WithData("customerName", customerName);
        }
    }
}

