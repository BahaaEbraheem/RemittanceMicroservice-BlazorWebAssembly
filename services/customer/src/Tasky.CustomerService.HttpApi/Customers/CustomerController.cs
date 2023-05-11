﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Tasky.CustomerService.Customers.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Tasky.CustomerService.Customers;
[Area(CustomerServiceRemoteServiceConsts.ModuleName)]
[RemoteService(Name = CustomerServiceRemoteServiceConsts.RemoteServiceName)]
[Route("api/customerService/customer")]
public class CustomerController : CustomerServiceController, ICustomerAppService
{
    private readonly ICustomerAppService _customerAppService;

    public CustomerController(ICustomerAppService customerAppService)
    {
        _customerAppService = customerAppService;
    }

    [HttpGet]
    [Route("CreateAsync")]

    public Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input)
    {
        return _customerAppService.CreateAsync(input);
    }
    [HttpGet("DeleteAsync")]

    public Task DeleteAsync(Guid id)
    {
        return _customerAppService.DeleteAsync(id);
    }
    //[HttpGet]
    //[Route("GetAllAsync")]
    //public Task<List<CustomerDto>> GetAllAsync()
    //{
    //    return _customerAppService.GetAllAsync();
    //}

    [HttpGet]
    [Route("GetAsync")]

    public Task<CustomerDto> GetAsync(Guid id)
    {
        return _customerAppService.GetAsync(id);
    }
    [HttpGet]
    [DisableValidation]
    [Route("GetListAsync")]
    public virtual async Task<PagedResultDto<CustomerDto>> GetListAsync(CustomerPagedAndSortedResultRequestDto input)
    {
        return await _customerAppService.GetListAsync(input);
    }

    //[HttpGet]
    //[Route("GetFromReposListAsync")]
    //public Task<List<CustomerDto>> GetFromReposListAsync(int skipCount, int maxResultCount, string sorting, CustomerDto filter)
    //{
    //    return _customerAppService.GetFromReposListAsync(skipCount, maxResultCount, sorting, filter);
    //}
    //[HttpGet]
    //[Route("GetTotalCountAsync")]
    //public Task<int> GetTotalCountAsync(CustomerDto filter)
    //{
    //    return _customerAppService.GetTotalCountAsync(filter);
    //}

    [HttpGet]
    [Route("UpdateAsync")]

    public Task<CustomerDto> UpdateAsync(Guid id, CreateUpdateCustomerDto input)
    {
        return _customerAppService.UpdateAsync(id, input);
    }
}