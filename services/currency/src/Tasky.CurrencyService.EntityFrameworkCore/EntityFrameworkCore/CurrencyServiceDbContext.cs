using Microsoft.EntityFrameworkCore;
using Tasky.CurrencyService.Currencies;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tasky.CurrencyService.EntityFrameworkCore;

[ConnectionStringName(CurrencyServiceDbProperties.ConnectionStringName)]
public class CurrencyServiceDbContext : AbpDbContext<CurrencyServiceDbContext>, ICurrencyServiceDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<Currency> Currencies { get; set; }
    public CurrencyServiceDbContext(DbContextOptions<CurrencyServiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureCurrencyService();
    }
}
