using Microsoft.EntityFrameworkCore;
using Tasky.RemittanceService.Remittances;
using Tasky.RemittanceService.Status;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tasky.RemittanceService.EntityFrameworkCore;

[ConnectionStringName(RemittanceServiceDbProperties.ConnectionStringName)]
public class RemittanceServiceDbContext : AbpDbContext<RemittanceServiceDbContext>, IRemittanceServiceDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<Remittance> Remittances { get; set; }
    public DbSet<RemittanceStatus> RemittanceStatus { get; set; }
    public RemittanceServiceDbContext(DbContextOptions<RemittanceServiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureRemittanceService();
    }
}
