using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;
using Volo.Abp.Domain.Entities.Auditing;
﻿using JetBrains.Annotations;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Tasky.AmlService.Aml_Person
{
    public class AmlPerson : FullAuditedAggregateRoot<Guid>
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }
        public AmlPerson(Guid id, string firstName, string lastName, string fatherName, string motherName)
        {
            Id = id;
            FirstName = firstName;
            FatherName = fatherName;
            LastName = lastName;
            MotherName = motherName;
        }

    }
}


