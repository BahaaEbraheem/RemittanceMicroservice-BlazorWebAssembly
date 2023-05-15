using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Volo.Abp.ObjectMapping;
using Volo.Abp.DependencyInjection;
using Tasky.RemittanceService.Status.Dtos;

namespace Tasky.RemittanceService.Status
{
    public class RemittanceStatusAppService :
        CrudAppService<
               RemittanceStatus, //The customer entity
            RemittanceStatusDto, //Used to show customers
            Guid, //Primary key of the customer entity
            RemittanceStatusPagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateRemittanceStatusDto>, //Used to create/update a customer
        IRemittanceStatusAppService //implement the IcustomerAppService
    {
        public RemittanceStatusAppService(IRemittanceStatusRepository Repository) :
            base(Repository)
        {

        }

    }

}



