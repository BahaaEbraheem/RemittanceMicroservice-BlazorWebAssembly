using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Tasky.CustomerService.Customers
{
    public class CustomerExceptionInHisBirthDate : BusinessException
    {
        public CustomerExceptionInHisBirthDate(DateTime birthDate) : base(CustomerServiceErrorCodes.BirthDateExeption)
        {
            WithData("birthDate", birthDate);
        }
    }
}

