using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Tasky.CustomerService.Customers
{
    public class CustomerDontPassBecauseHisAgeSmallerThan18Exception : BusinessException
    {
        public CustomerDontPassBecauseHisAgeSmallerThan18Exception(string customerName)
            : base(CustomerServiceErrorCodes.CustomerDontPassBecauseHisAgeSmallerThan18)
        {
            WithData("customerName", customerName);
        }

    }
}
