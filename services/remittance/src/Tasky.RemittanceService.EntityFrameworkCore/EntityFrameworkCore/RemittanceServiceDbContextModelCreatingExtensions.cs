using Microsoft.EntityFrameworkCore;
using Tasky.RemittanceService.Remittances;
using Tasky.RemittanceService.Status;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tasky.RemittanceService.EntityFrameworkCore;

public static class RemittanceServiceDbContextModelCreatingExtensions
{
    public static void ConfigureRemittanceService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Remittance>(b => {
            b.ToTable(RemittanceServiceDbProperties.DbTablePrefix + "Remittances" + RemittanceServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.HasKey(x => new { x.Id });
            b.Property(x => x.Amount).IsRequired();
            b.Property(x => x.Type).IsRequired();
            b.Property(x => x.ReceiverFullName).IsRequired();

            b.Property(x => x.CurrencyId);

            b.Property(x => x.SenderBy).IsRequired();
            b.Property(x => x.ReceiverBy);

            b.Property(x => x.CreatorId);
            b.Property(x => x.ApprovedBy);
            b.Property(x => x.ReleasedBy);

            //one-to-many relationship with IdentityUser table
            //b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.CreatorId);
            //  b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.ApprovedBy);
            //  b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.ReleasedBy);

            //one-to-many relationship with Customer table

            //b.HasOne<Customer>().WithMany().HasForeignKey(x => x.SenderBy).IsRequired();
            //b.HasOne<Customer>().WithMany().HasForeignKey(x => x.ReceiverBy);

            //one-to-many relationship with Currency table

            //b.HasOne<Currency>().WithMany().HasForeignKey(x => x.CurrencyId).IsRequired();
            //many-to-many relationship with RemittanceStatus table 
            b.HasMany(x => x.Status).WithOne().HasForeignKey(x => x.RemittanceId);


            b.HasIndex(e => new { e.SerialNumber }).IsUnique();
        });


        builder.Entity<RemittanceStatus>(b => {
            b.ToTable(RemittanceServiceDbProperties.DbTablePrefix + "RemittanceStatus" + RemittanceServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.HasKey(x => new { x.Id });

            b.Property(x => x.State).IsRequired();

            //one-to-many relationship with Remittance table
            b.HasOne<Remittance>().WithMany(x => x.Status).HasForeignKey(x => x.RemittanceId).IsRequired();
            //one-to-many relationship with IdentityUser table
            //b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.CreatorId).IsRequired();

        });
    }
}
