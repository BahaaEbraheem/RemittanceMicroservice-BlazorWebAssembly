using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Tasky.CurrencyService.EntityFrameworkCore;
using Tasky.CurrencyService.Currencies;

namespace Tasky.CurrencyService.Currencies
{
    public class EfCoreCurrencyRepository : EfCoreRepository<CurrencyServiceDbContext, Currency, Guid>, ICurrencyRepository
    {
        public EfCoreCurrencyRepository(
            IDbContextProvider<CurrencyServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Currency> FindByNameAndSymbolAsync(string name, string symbol)
        {
            var dbSet = await GetDbSetAsync();
            var firstCondition = dbSet.FirstOrDefaultAsync(Currency => Currency.Name.Equals(name)
           && Currency.Symbol.Equals(symbol)).Result;
            if (firstCondition != null)
                return firstCondition;
            var SecondCondition = dbSet.FirstOrDefaultAsync(Currency => name.Contains(Currency.Name)
          && symbol.Contains(Currency.Symbol)).Result;
            if (SecondCondition != null)
                return SecondCondition;
            var existCurrency = dbSet.FirstOrDefaultAsync(Currency =>
            Currency.Name.Contains(name)
            && Currency.Symbol.Contains(symbol)).Result;
            if (existCurrency != null)
                return existCurrency;
            return null;
        }

        public async Task<List<Currency>> GetAllAsync()
        {
            var dbSet = GetDbSetAsync();
            var currencies = await dbSet.Result.ToListAsync();
            return currencies;
        }
        public Task<List<Currency>> GetFromReposListAsync(int skipCount, int maxResultCount, string sorting, Currency filter)
        {

            var dbSet = GetDbSetAsync();
            var currencies = dbSet.Result
                .WhereIf(!filter.Name.IsNullOrWhiteSpace(),
                x => x.Name.Contains(filter.Name))
                .WhereIf(!filter.Symbol.IsNullOrWhiteSpace(),
                x => x.Symbol.Contains(filter.Symbol))
                .WhereIf(!filter.Code.IsNullOrWhiteSpace(),
                x => x.Code.Contains(filter.Code)).OrderBy(sorting)
                .Skip(skipCount).Take(maxResultCount).ToListAsync();
            return currencies;
        }

        public async Task<int> GetTotalCountFromReposAsync(Currency filter)
        {
            var dbSet = await GetDbSetAsync();
            var currencies = await dbSet
                .WhereIf(!filter.Name.IsNullOrWhiteSpace(),
                x => x.Name.Contains(filter.Name))
                .WhereIf(!filter.Symbol.IsNullOrWhiteSpace(),
                x => x.Symbol.Contains(filter.Symbol))
                .WhereIf(!filter.Code.IsNullOrWhiteSpace()
                , x => x.Code.Contains(filter.Code))
                .ToListAsync();
            return currencies.Count;
        }


    }
}

