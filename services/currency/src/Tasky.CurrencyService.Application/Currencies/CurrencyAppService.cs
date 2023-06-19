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
using Volo.Abp.ObjectMapping;
using Volo.Abp.DependencyInjection;
using static System.Reflection.Metadata.BlobBuilder;
//using Volo.Abp.SettingManagement;
using Volo.Abp;
using System.Xml.Linq;
using System.Reflection;
using Volo.Abp.Authorization.Permissions;
using Tasky.CurrencyService.Permissions;
using Volo.Abp.Features;
using Tasky.CurrencyService.Features;
using Tasky.RemittanceService.Remittances;
using Polly.Retry;
using Polly;
using Polly.CircuitBreaker;
using Tasky.Microservice.Shared.Dtos;
using System.Net.Http;
//using Currency.Remittances;

namespace Tasky.CurrencyService.Currencies
{
    [Authorize(CurrencyServicePermissions.Currencies.Default)]
    public class CurrencyAppService :
        CrudAppService<
            Currency, //The Currency entity
            CurrencyDto, //Used to show Currencies
            Guid, //Primary key of the Currency entity
            CurrencyPagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateCurrencyDto>, //Used to create/update a Currency
        ICurrencyAppService //implement the ICurrencyAppService
    {

        private readonly ICurrencyRepository _currencyRepository;
        private readonly IRemittanceAppService _remittanceAppService;
        private readonly IPermissionChecker _permissionChecker;
        private readonly CurrencyManager _currencyManager;
        public CurrencyAppService(
            ICurrencyRepository currencyRepository,
            IRemittanceAppService remittanceAppService,
            IPermissionChecker permissionChecker,
            CurrencyManager currencyManager)
    : base(currencyRepository)
        {

            _permissionChecker = permissionChecker;
            _currencyRepository = currencyRepository;
            _currencyManager = currencyManager;
            _remittanceAppService = remittanceAppService;
        }



        public override Task<CurrencyDto> GetAsync(Guid id)
        {
            return base.GetAsync(id);
        }
        public override async Task<PagedResultDto<CurrencyDto>> GetListAsync(CurrencyPagedAndSortedResultRequestDto input)
        {


            var filter = ObjectMapper.Map<CurrencyPagedAndSortedResultRequestDto, CurrencyDto>(input);
            var sorting = (string.IsNullOrEmpty(input.Sorting) ? "Name DESC" : input.Sorting).Replace("ShortName", "Name");

            var currencies = await GetFromReposListAsync(input.SkipCount, input.MaxResultCount, sorting, filter);
            var totalCount = await GetTotalCountAsync(filter);
            return new PagedResultDto<CurrencyDto>(totalCount, currencies);
        }

        [Authorize(CurrencyServicePermissions.Currencies.Create)]
        public override async Task<CurrencyDto> CreateAsync(CreateUpdateCurrencyDto input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("");
            }
            Check.NotNullOrWhiteSpace(input.Name, nameof(input.Name));
            Check.NotNullOrWhiteSpace(input.Symbol, nameof(input.Symbol));
            var existingCurrency = FindByNameAndSymbolAsync(input.Name, input.Symbol).Result;
            if (existingCurrency != null)
            {
                throw new UserFriendlyException("Currency Exist Befor");
            }
            return await base.CreateAsync(input);
        }

        [Authorize(CurrencyServicePermissions.Currencies.Update)]
        public override async Task<CurrencyDto> UpdateAsync(Guid id, CreateUpdateCurrencyDto input)
        {

            if (input == null)
            {
                throw new ArgumentNullException("");
            }
            Check.NotNullOrWhiteSpace(id.ToString(), nameof(id));
            //check if currency exist befor in tables
            var existingCurrency = await FindByNameAndSymbolAsync(input.Name, input.Symbol);
            if ((existingCurrency != null && !existingCurrency.Name.Contains(input.Name))
               || (existingCurrency != null && !existingCurrency.Symbol.Contains(input.Symbol)))
            {
                throw new UserFriendlyException("Currency Exist Befor");
            }
            return await base.UpdateAsync(id, input);
        }



        [Authorize(CurrencyServicePermissions.Currencies.Delete)]
        public override async Task DeleteAsync(Guid id)
        {
            //check if this currency using by any remittance
            try
            {

                var remittancequeryable = await _remittanceAppService.GetAllAsync();
                var remittance = remittancequeryable.Where(a => a.CurrencyId == id).FirstOrDefault();
                if (remittance != null)
                {
                    string remittanceSerial = remittance.SerialNumber;
                    throw new UserFriendlyException("Currency Used By Saving Remittance");
                }
                await base.DeleteAsync(id);
            }
            catch (Exception)
            {

                HandleBrokenCircuitException();
            }


        }
    
        private void HandleBrokenCircuitException()
        {
            Console.WriteLine("Remittance Service is inoperative, please try later on. (Business message due to Circuit-Breaker");
        }
    










        public async Task<CurrencyDto> FindByNameAndSymbolAsync(string name, string symbol)
        {
            var currencyDto = await _currencyRepository.FindByNameAndSymbolAsync(name, symbol);
           return ObjectMapper.Map<Currency, CurrencyDto>(currencyDto);
        }

     

        public async Task<int> GetTotalCountAsync(CurrencyDto filter)
        {
            return await _currencyRepository.GetTotalCountFromReposAsync(
                ObjectMapper.Map<CurrencyDto,Currency>(filter));
        }

        public  async Task<List<CurrencyDto>> GetAllAsync()
        {
              return  ObjectMapper.Map<List<Currency>, List<CurrencyDto>>(await _currencyRepository.GetAllAsync());
        }

      public async Task<List<CurrencyDto>> GetFromReposListAsync(int skipCount, int maxResultCount, string sorting, CurrencyDto filter)
        {
            var filter_ = ObjectMapper.Map<CurrencyDto, Currency>(filter);
           var currencies =await _currencyRepository.GetFromReposListAsync(skipCount, maxResultCount, sorting, filter_);
           return  ObjectMapper.Map<List<Currency>, List<CurrencyDto>>(currencies);
        }
    }
}


