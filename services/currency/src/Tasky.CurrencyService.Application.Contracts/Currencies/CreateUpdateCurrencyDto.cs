using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Data;

namespace Tasky.CurrencyService.Currencies
{
    public class CreateUpdateCurrencyDto
    {


        [Required]
        public string Name { get; set; }

        [Required]
        public string Symbol { get; set; }
        public string Code { get; set; }
        //public DateTime? LastModificationTime { get; set; }

        //public Guid? LastModifierId { get; set; }

        //public DateTime CreationTime { get; set; }

        //public Guid? CreatorId { get; set; }

    }
}
