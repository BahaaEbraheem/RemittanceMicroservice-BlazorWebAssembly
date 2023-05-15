﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using static Tasky.Microservice.Shared.Enums.Enums;

namespace Tasky.Microservice.Shared.Etos
{
    public class RemittanceEto
    {
        public Guid Id { get; set; }
        public Guid RemittanceId { get; set; }
        public double Amount { get; set; }

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

        public DateTime? LastModificationTime { get; set; }

        public Guid? LastModifierId { get; set; }

        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }



    }
}