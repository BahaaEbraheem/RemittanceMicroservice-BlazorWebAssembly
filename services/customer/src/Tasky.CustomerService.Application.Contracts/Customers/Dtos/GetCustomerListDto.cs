﻿using Volo.Abp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tasky.CustomerService.Customers.Dtos
{
    public class GetCustomerListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}