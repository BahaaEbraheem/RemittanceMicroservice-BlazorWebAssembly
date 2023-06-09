﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasky.CustomerService.Customers.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Tasky.CustomerService.Customers
{
    public interface ICustomerRepository : IRepository<Customer, Guid>
    {
        Task<List<Customer>> GetAllAsync();

        //Task<Customer> FindByFullNameAsync(string firstName, string lastName,string fatherName,string motherName);

        Task<List<Customer>> GetFromReposListAsync(
     int skipCount,
     int maxResultCount,
     string sorting,
     Customer filter
 );
        Task<int> GetTotalCountAsync(Customer filter);
    }
}