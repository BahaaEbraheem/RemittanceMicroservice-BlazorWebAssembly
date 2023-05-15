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
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;
using Tasky.CustomerService.Permissions;
using Tasky.CustomerService.Customers.Dtos;

namespace Tasky.CustomerService.Customers
{
    [Authorize(CustomerServicePermissions.Customers.Default)]

    public class CustomerAppService : 
         CrudAppService<
                Customer, //The customer entity
             CustomerDto, //Used to show customers
             Guid, //Primary key of the customer entity
             CustomerPagedAndSortedResultRequestDto, //Used for paging/sorting
             CreateUpdateCustomerDto>, //Used to create/update a customer
         ICustomerAppService //implement the IcustomerAppService
    {
        private readonly CustomerManager _customerManager;
        private static IRepository<Customer, Guid>? _customerRepository;

        public CustomerAppService(CustomerManager customerManager, IRepository<Customer, Guid> customerRepository) : base(customerRepository)
        {
            _customerManager = customerManager;
            _customerRepository = customerRepository;
        }
        public override async Task<PagedResultDto<CustomerDto>> GetListAsync(CustomerPagedAndSortedResultRequestDto input)
        {
            var filter = ObjectMapper.Map<CustomerPagedAndSortedResultRequestDto, CustomerDto>(input);
            var sorting = (string.IsNullOrEmpty(input.Sorting) ? "FirstName DESC" : input.Sorting).Replace("ShortName", "FirstName");
            var customers = await _customerManager.GetFromReposListAsync(input.SkipCount, input.MaxResultCount, sorting, filter);
            var totalCount = await _customerManager.GetTotalCountAsync(filter);
            return new PagedResultDto<CustomerDto>(totalCount, customers);
        }

        [Authorize(CustomerServicePermissions.Customers.Create)]
        public override Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input)
        {
            try
            {
                if (input != null)
                {
                    return base.CreateAsync(input);

                }
                throw new ArgumentNullException(nameof(input));

            }
            catch (Exception)
            {

                throw;
            }
        }



        [Authorize(CustomerServicePermissions.Customers.Update)]
        public override Task<CustomerDto> UpdateAsync(Guid id, CreateUpdateCustomerDto input)
        {
            try
            {
                if (input != null)
                {
                    return base.UpdateAsync(id, input);

                }
                throw new ArgumentNullException(nameof(input));
            }
            catch (Exception)
            {

                throw;
            }
        }


        [Authorize(CustomerServicePermissions.Customers.Delete)]
        public override async Task DeleteAsync(Guid id)
        {
            try
            {
                if (!id.Equals(null))
                {
                    await _customerManager.IsCustomerUsedBeforInRemittance(id);

                    await base.DeleteAsync(id);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Task<List<CustomerDto>> GetAllAsync()
        {
            return _customerManager.GetAllAsync();
        }
    }
}
