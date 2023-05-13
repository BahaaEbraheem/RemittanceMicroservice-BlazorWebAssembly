using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Tasky.CurrencyService.Currencies
{
   [Area(CurrencyServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = CurrencyServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/currencyService/currency")]
    public class CurrencyController : CurrencyServiceController, ICurrencyAppService
    {
        private readonly ICurrencyAppService _currencyAppService;

        public CurrencyController(ICurrencyAppService currencyAppService)
        {
            _currencyAppService = currencyAppService;
        }

        //[HttpGet]
        //public async Task<List<ProjectDto>> GetAllAsync()
        //{
        //    return await _CurrencyService.GetAllAsync();
        //}


        //[HttpPost]
        //public async Task<ProjectDto> Create(ProjectDto projectDto)
        //{
        //    return await _CurrencyService.Create(projectDto);
        //}


        [HttpGet]
        [Route("GetAsync")]
        public virtual async Task<CurrencyDto> GetAsync(Guid id)
        {
            return await _currencyAppService.GetAsync(id);
        }
        [HttpGet]
        [DisableValidation]
        [Route("GetListAsync")]
        //[HttpGet("GetListAsync")]
        public virtual async Task<PagedResultDto<CurrencyDto>> GetListAsync(CurrencyPagedAndSortedResultRequestDto input)
        {
            return await _currencyAppService.GetListAsync(input);
        }







        [HttpPost]
        [Route("CreateAsync")]
        public virtual async Task<CurrencyDto> CreateAsync(CreateUpdateCurrencyDto input)
        {
            return await _currencyAppService.CreateAsync(input);
        }
        [HttpGet]
        [Route("UpdateAsync")]
        public async Task<CurrencyDto> UpdateAsync(Guid id, CreateUpdateCurrencyDto input)
        {
            return await _currencyAppService.UpdateAsync(id, input);
        }
        [HttpDelete]
        [Route("DeleteAsync")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _currencyAppService.DeleteAsync(id);
        }
        [HttpGet]
        [Route("FindByNameAndSymbolAsync")]
        public Task<CurrencyDto> FindByNameAndSymbolAsync(string name, string symbol)
        {
            return _currencyAppService.FindByNameAndSymbolAsync(name, symbol);
        }

        [HttpGet]
        [Route("GetAllAsync")]
        public async Task<List<CurrencyDto>> GetAllAsync()
        {
            return await _currencyAppService.GetAllAsync();
        }
        //[HttpGet]
        //[Route("GetRemittanceLookupAsync")]
        //public async Task<ListResultDto<RemittanceLookupDto>> GetRemittanceLookupAsync()
        //{
        //    return await _currencyAppService.GetRemittanceLookupAsync();
        //}

    }

}
