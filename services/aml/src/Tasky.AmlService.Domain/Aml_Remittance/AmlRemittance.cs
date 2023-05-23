﻿using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using static Tasky.Microservice.Shared.Enums.Enums;

namespace Tasky.AmlService.Aml_Remittance
{
    public class AmlRemittance : FullAuditedAggregateRoot<Guid>
    {
        public double Amount { get; set; }
        public Guid RemittanceId { get; set; }

        public RemittanceType Type { get; set; }
        public string SerialNumber { get; set; }

        public Guid SenderBy { get; set; }
        public Guid? CurrencyId { get; set; }
        public Remittance_Status State { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }
    }
}


