﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Volo.Abp.Domain.Services;
using Tasky.AmlService.EntityFrameworkCore;

namespace Tasky.AmlService.Aml_Person
{
    public class AmlPersonRepository : EfCoreRepository<AmlServiceDbContext, AmlPerson, Guid>, IAmlPersonRepository
    {
        public AmlPersonRepository(
            IDbContextProvider<AmlServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<AmlPerson> GetAmlPersonByFirstAndFatherAndLastName(string firstName, string fatherName, string lastName)
        {
            var dbSet = await GetDbSetAsync();
            var checkAmlPerson = dbSet
                .WhereIf(!firstName.IsNullOrWhiteSpace(), aml => aml.FirstName == firstName || aml.FirstName.Contains(firstName))
                .WhereIf(!fatherName.IsNullOrWhiteSpace(), aml => aml.FatherName == firstName || aml.FatherName.Contains(fatherName))
                .WhereIf(!lastName.IsNullOrWhiteSpace(), aml => aml.LastName == firstName || aml.LastName.Contains(lastName)).FirstOrDefault();
            return checkAmlPerson;


        }







    }
}