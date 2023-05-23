using Microsoft.EntityFrameworkCore;
using Tasky.AmlService.Aml_Person;
using Tasky.AmlService.Aml_Remittance;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tasky.AmlService.EntityFrameworkCore;

public static class AmlServiceDbContextModelCreatingExtensions
{
    public static void ConfigureAmlService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));



        builder.Entity<AmlRemittance>(b =>
        {
            b.ToTable(AmlServiceDbProperties.DbTablePrefix + "AmlRemittances", AmlServiceDbProperties.DbSchema);

            b.ConfigureByConvention();
        });
        builder.Entity<AmlPerson>(b =>
        {
            b.ToTable(AmlServiceDbProperties.DbTablePrefix + "AmlPersons", AmlServiceDbProperties.DbSchema);

            b.ConfigureByConvention();
        });

    }
}
