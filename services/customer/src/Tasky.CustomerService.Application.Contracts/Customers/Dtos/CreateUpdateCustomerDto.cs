﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Tasky.Microservice.Shared.Enums.Enums;

namespace Tasky.CustomerService.Customers.Dtos
{
    public class CreateUpdateCustomerDto : IValidatableObject
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]

        public string? LastName { get; set; }
        [Required]

        public string? FatherName { get; set; }
        [Required]

        public string? MotherName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required]

        public string? Phone { get; set; }
            
        public string? Address { get; set; }
        [Required]

        public Gender Gender { get; set; }
        public DateTime? LastModificationTime { get; set; }

        public Guid? LastModifierId { get; set; }
        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BirthDate > DateTime.Now)
            {
                yield return new ValidationResult(
                    "The date of birth cannot be greater than today's date 0!",
                    new[] { "BirthDate" }
                );
            }
        }
    }
}