﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tasky.Microservice.Shared.Dtos
{
    public class RemittanceLookupDto : EntityDto<Guid>
    {
        public new Guid Id { get; set; }
        public Guid SenderBy { get; set; }
        public string SerialNumber { get; set; }

        public Guid? CurrencyId { get; set; }

    }
}