using Microsoft.EntityFrameworkCore;
using Tasky.CustomerService.Customers;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tasky.CustomerService.EntityFrameworkCore;

public static class CustomerServiceDbContextModelCreatingExtensions
{
    public static void ConfigureCustomerService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Customer>(b => {
            b.ToTable(CustomerServiceDbProperties.DbTablePrefix + "Customers" + CustomerServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.HasKey(x => new { x.Id });
            b.Property(x => x.FirstName).IsRequired();
            b.Property(x => x.LastName).IsRequired();
            b.Property(x => x.FatherName).IsRequired();
            b.Property(x => x.MotherName).IsRequired();
            b.Property(x => x.BirthDate).IsRequired();
            b.Property(x => x.Phone).IsRequired();
            b.Property(x => x.Gender).IsRequired();
            b.HasIndex(e => new { e.FirstName, e.LastName, e.FatherName, e.MotherName }).IsUnique();
        });
    }
}
