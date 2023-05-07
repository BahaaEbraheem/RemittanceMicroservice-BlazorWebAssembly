using Microsoft.EntityFrameworkCore;
using Tasky.CurrencyService.Currencies;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tasky.CurrencyService.EntityFrameworkCore;

[ConnectionStringName(CurrencyServiceDbProperties.ConnectionStringName)]
public interface ICurrencyServiceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    public DbSet<Currency> Currencies { get; set; }

}
