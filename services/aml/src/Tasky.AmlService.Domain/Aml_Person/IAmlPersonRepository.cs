using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Tasky.AmlService.Aml_Person
{
    public interface IAmlPersonRepository : IRepository<AmlPerson, Guid>, IDomainService
    {
        Task<AmlPerson> GetAmlPersonByFirstAndFatherAndLastName(string firstName, string fatherName, string lastName);
    }

}