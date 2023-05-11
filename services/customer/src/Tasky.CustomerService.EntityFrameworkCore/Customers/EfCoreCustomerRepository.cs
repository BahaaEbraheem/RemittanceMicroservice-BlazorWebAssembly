﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Volo.Abp.ObjectMapping;
using Tasky.CustomerService.Customers.Dtos;
using Tasky.CustomerService.EntityFrameworkCore;

namespace Tasky.CustomerService.Customers
{
    public class EfCoreCustomerRepository : EfCoreRepository<CustomerServiceDbContext, Customer, Guid>, ICustomerRepository
    {
        private readonly IObjectMapper _ObjectMapper;

        public EfCoreCustomerRepository(
            IObjectMapper ObjectMapper,
            IDbContextProvider<CustomerServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
            _ObjectMapper = ObjectMapper;
        }

        //public async Task<Customer> FindByFullNameAsync(string firstName, string lastName, string fatherName, string motherName)
        //{
        //    var dbSet = await GetDbSetAsync();
        //    return await dbSet.FirstOrDefaultAsync(Customer => Customer.FirstName == firstName && Customer.LastName == lastName &&
        //                                            Customer.FatherName == fatherName && Customer.MotherName == motherName);
        //}
        public async Task<List<Customer>> GetAllAsync()
        {
            var dbSet = await GetDbSetAsync();
            var customers = await dbSet.ToListAsync();
            return customers;
        }
        public async Task<List<Customer>> GetFromReposListAsync(int skipCount, int maxResultCount, string sorting, Customer filter)
        {
            var dbSet = await GetDbSetAsync();

            var customers = await dbSet
                .WhereIf(!filter.FirstName.IsNullOrWhiteSpace(), x => x.FirstName.Contains(filter.FirstName))
                .WhereIf(!filter.LastName.IsNullOrWhiteSpace(), x => x.LastName.Contains(filter.LastName))
                .WhereIf(!filter.FatherName.IsNullOrWhiteSpace(), x => x.FatherName.Contains(filter.FatherName))
                .WhereIf(!filter.MotherName.IsNullOrWhiteSpace(), x => x.MotherName.Contains(filter.MotherName))
                .OrderBy(sorting).Skip(skipCount).Take(maxResultCount).ToListAsync();
            return customers/* _ObjectMapper.Map<List<Customer>, List<Customer>>(customers)*/;

        }



        public async Task<int> GetTotalCountAsync(Customer filter)
        {
            var dbSet = await GetDbSetAsync();
            var customers = await dbSet
                .WhereIf(!filter.FirstName.IsNullOrWhiteSpace(),
                x => x.FirstName.Contains(filter.FirstName))
                .WhereIf(!filter.LastName.IsNullOrWhiteSpace(),
                x => x.LastName.Contains(filter.LastName))
                .WhereIf(!filter.FatherName.IsNullOrWhiteSpace(),
                x => x.FatherName.Contains(filter.FatherName))
                .WhereIf(!filter.MotherName.IsNullOrWhiteSpace()
                , x => x.MotherName.Contains(filter.MotherName))
                .ToListAsync();
            return customers.Count;
        }


    }
}
