﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tasky.Microservice.Shared.Dtos
{
    public class CurrencyLookupDto : EntityDto<Guid>
    {
        public new Guid? Id { get; set; }

        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Code { get; set; }
    }
}