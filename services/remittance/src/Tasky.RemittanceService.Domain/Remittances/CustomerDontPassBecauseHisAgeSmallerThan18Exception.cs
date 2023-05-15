﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Tasky.RemittanceService.Remittances
{
    public class CustomerDontPassBecauseHisAgeSmallerThan18Exception : BusinessException
    {
        public CustomerDontPassBecauseHisAgeSmallerThan18Exception(string customerName)
            : base(RemittanceServiceErrorCodes.CustomerDontPassBecauseHisAgeSmallerThan18)
        {
            WithData("customerName", customerName);
        }

    }
}