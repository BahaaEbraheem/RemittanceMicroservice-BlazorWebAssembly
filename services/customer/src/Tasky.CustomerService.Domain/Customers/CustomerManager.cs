
//using RemittanceManagement.Remittances;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Tasky.CustomerService.Customers
{
    public class CustomerManager : DomainService
    {
        private readonly ICustomerRepository _CustomerRepository;
        //private readonly IRemittanceAppService _remittanceRepository;


        public CustomerManager(ICustomerRepository CustomerRepository
            //,IRemittanceAppService remittanceRepository
            )
        {
            _CustomerRepository = CustomerRepository;
            //_remittanceRepository = remittanceRepository;
        }



        public async Task IsCustomerUsedBeforInRemittance(Guid id)
        {
            Check.NotNull(id, nameof(id));
            //var remittancequeryable = await _remittanceRepository.GetListAsync(new GetRemittanceListDto());
            //var remittance = remittancequeryable.Items.Where(a => (a.SenderBy == id || a.ReceiverBy == id)).FirstOrDefault();
            //if (remittance != null)
            //{
            //    var firstName = _CustomerRepository.GetAsync(id).Result.FirstName;
            //    var lastName = _CustomerRepository.GetAsync(id).Result.LastName;
            //    var customerName = firstName + " " + lastName;
            //    throw new UserFriendlyException("this Customer Used Befor In Remittance");
            //}
            await Task.CompletedTask;
        }

    }
}