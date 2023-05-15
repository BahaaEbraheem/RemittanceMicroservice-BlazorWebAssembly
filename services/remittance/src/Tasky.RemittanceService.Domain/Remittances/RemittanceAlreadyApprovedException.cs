﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Tasky.RemittanceService.Remittances
{
    public class RemittanceAlreadyApprovedException : BusinessException
    {
        public RemittanceAlreadyApprovedException() : base(RemittanceServiceErrorCodes.RemittanceAlreadyApproved)
        {
            //WithData( );
        }
    }
}