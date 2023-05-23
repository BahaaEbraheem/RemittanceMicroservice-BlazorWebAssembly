using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Volo.Abp.Validation;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;
using Tasky.AmlService.Aml_Person;

namespace Tasky.AmlService.Aml_Remittance
{
    public class AmlRemittanceManager : DomainService
    {
        private readonly IAmlPersonRepository _amlPersonRepository;
        private readonly IAmlRemittanceRepository _amlRemittanceRepository;
        public AmlRemittanceManager(IAmlPersonRepository amlPersonRepository,
            IAmlRemittanceRepository amlRemittanceRepository)
        {
            _amlRemittanceRepository = amlRemittanceRepository;
            _amlPersonRepository = amlPersonRepository;
        }
        public async Task UpdateAsync(AmlRemittance remittance)
        {
            await _amlRemittanceRepository.UpdateAsync(remittance);
        }


        public async Task<AmlRemittance> GetAsync(Guid id)
        {
            return await _amlRemittanceRepository.GetAsync(id);
        }

        public async Task CreateAsync(AmlRemittance input)
        {
            await _amlRemittanceRepository.InsertAsync(input);
        }

        public async Task<AmlPerson> GetAmlPersonByFirstAndFatherAndLastName(string firstName, string fatherName, string lastName)
        {
            return await _amlPersonRepository.GetAmlPersonByFirstAndFatherAndLastName(firstName, fatherName, lastName);
        }
    }
}