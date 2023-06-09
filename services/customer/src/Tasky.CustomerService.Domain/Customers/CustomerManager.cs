﻿//using RemittanceService.Remittances;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasky.CustomerService.Customers.Dtos;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.ObjectMapping;
using static Tasky.CustomerService.Permissions.CustomerServicePermissions;

namespace Tasky.CustomerService.Customers
{
    public class CustomerManager :DomainService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IObjectMapper _objectMapper;


        public CustomerManager(ICustomerRepository customerRepository
            , IObjectMapper objectMapper
           
            )
        {
            _customerRepository = customerRepository;
            _objectMapper = objectMapper;
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            return _objectMapper.Map<List<Customer>, List<CustomerDto>>(await _customerRepository.GetAllAsync());

        }

        public async Task<List<CustomerDto>> GetFromReposListAsync(int skipCount, int maxResultCount, string sorting, CustomerDto filter)
        {
            var filter_ = _objectMapper.Map<CustomerDto, Customer>(filter);
           var customers= _customerRepository.GetFromReposListAsync(skipCount, maxResultCount, sorting, filter_);
            return _objectMapper.Map<List<Customer>, List<CustomerDto>>( await customers);
        }

        public async Task<int> GetTotalCountAsync(CustomerDto filter)
        {
            return await _customerRepository.GetTotalCountAsync(_objectMapper.Map<CustomerDto, Customer>(filter));
        }

 


    }
}