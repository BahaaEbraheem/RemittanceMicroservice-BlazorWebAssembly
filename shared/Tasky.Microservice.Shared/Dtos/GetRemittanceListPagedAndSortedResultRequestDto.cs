﻿using Volo.Abp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using static Tasky.Microservice.Shared.Enums.Enums;

namespace Tasky.Microservice.Shared.Dtos
{
    public class GetRemittanceListPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        //public Guid? Id { get; set; }

        public double? Amount { get; set; }
        public double? TotalAmount { get; set; }
        public RemittanceType? Type { get; set; }
        public string? SerialNumber { get; set; }
        public string? SenderName { get; set; }
        public string? ReceiverFullName { get; set; }
        public string? CurrencyName { get; set; }
        public Remittance_Status? State { get; set; }
        public DateTime? StatusDate { get; set; }



        public Guid? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }
        public Guid? ReleasedBy { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public Guid SenderBy { get; set; }
        public Guid? ReceiverBy { get; set; }
        public Guid CurrencyId { get; set; }
        public DateTime? LastModificationTime { get; set; }

        public Guid? LastModifierId { get; set; }
        public DateTime? CreationTime { get; set; }

        public Guid? CreatorId { get; set; }
        public bool? IsDeleted { get; set; }

        public Guid? DeleterId { get; set; }

        public DateTime? DeletionTime { get; set; }
        //public ICollection<RemittanceStatus> Status { get; set; }


    }
}