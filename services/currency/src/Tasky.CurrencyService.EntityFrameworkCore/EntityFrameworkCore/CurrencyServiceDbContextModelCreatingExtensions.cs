using Microsoft.EntityFrameworkCore;
using Tasky.CurrencyService.Currencies;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tasky.CurrencyService.EntityFrameworkCore;

public static class CurrencyServiceDbContextModelCreatingExtensions
{
    public static void ConfigureCurrencyService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));


        builder.Entity<Currency>(b =>
        {

            b.ToTable(CurrencyServiceDbProperties.DbTablePrefix + "Currencies" + CurrencyServiceDbProperties.DbSchema);
            b.ConfigureByConvention();

            b.HasKey(x => new { x.Id });

            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.Symbol).IsRequired();
            b.HasIndex(e => new { e.Name, e.Symbol }).IsUnique();


        });
    }
}
