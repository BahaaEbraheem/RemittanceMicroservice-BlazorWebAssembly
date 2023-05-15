using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tasky.CustomerService.Customers.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Tasky.CustomerService.Customers
{
    public interface ICustomerAppService : ITransientDependency,
         ICrudAppService< //Defines CRUD methods
             CustomerDto, //Used to show Customers
             Guid, //Primary key of the currency entity
             CustomerPagedAndSortedResultRequestDto, //Used for paging/sorting
             CreateUpdateCustomerDto> //Used to create/update a currency
    {
        Task<List<CustomerDto>> GetAllAsync();

        //Task<CustomerDto> FindByFullNameAsync(string firstName, string lastName, string fatherName, string motherName);

 //       Task<List<CustomerDto>> GetFromReposListAsync(
 //    int skipCount,
 //       int maxResultCount,
 //       string sorting,
 //    CustomerDto filter
 //);
        //Task<int> GetTotalCountAsync(CustomerDto filter);

    }
}