using System;
using System.Collections.Generic;
using System.Text;
using Tasky.RemittanceService.Status.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tasky.RemittanceService.Status
{
    public interface IRemittanceStatusAppService :
        ICrudAppService< //Defines CRUD methods
             RemittanceStatusDto, //Used to show currencies
             Guid, //Primary key of the currency entity
             RemittanceStatusPagedAndSortedResultRequestDto, //Used for paging/sorting
             CreateUpdateRemittanceStatusDto> //Used to create/update a currency
    {

    }
}