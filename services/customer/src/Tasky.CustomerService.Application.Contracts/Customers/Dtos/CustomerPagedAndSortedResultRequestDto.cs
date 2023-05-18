using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using static Tasky.Microservice.Shared.Enums.Enums;

namespace Tasky.CustomerService.Customers.Dtos
{
    public class CustomerPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FatherName { get; set; }

        public string? MotherName { get; set; }
        public DateTime BirthDate { get; set; }


        public string? Phone { get; set; }
        public string? Address { get; set; }

        public Gender Gender { get; set; } = Gender.Male;
        public DateTime? LastModificationTime { get; set; }

        public Guid? LastModifierId { get; set; }
        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }

    }
}
