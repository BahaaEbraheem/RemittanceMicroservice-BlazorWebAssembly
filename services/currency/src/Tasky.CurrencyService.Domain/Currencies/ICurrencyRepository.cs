using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tasky.CurrencyService.Currencies
{
    public interface ICurrencyRepository : IRepository<Currency,Guid>
    {
      Task<Currency> FindByNameAndSymbolAsync(string name, string symbol);
        Task<List<Currency>>GetAllAsync();
        Task<List<Currency>> GetFromReposListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            Currency filter
        );
        Task<int> GetTotalCountFromReposAsync(Currency filter);
    }
}
