using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tasky.CurrencyService.Currencies
{
    public class CurrencyDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Code { get; set; }



    }
}
