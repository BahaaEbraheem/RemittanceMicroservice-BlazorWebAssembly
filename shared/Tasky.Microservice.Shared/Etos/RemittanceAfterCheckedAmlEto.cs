﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Tasky.Microservice.Shared.Etos
{
    //[Serializable]
    public class RemittanceAfterCheckedAmlEto/*:EtoBase*/
    {
        public Guid RemittanceId { get; set; }

    }
}