using Microsoft.EntityFrameworkCore;
using Tasky.RemittanceService.Remittances;
using Tasky.RemittanceService.Status;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tasky.RemittanceService.EntityFrameworkCore;

[ConnectionStringName(RemittanceServiceDbProperties.ConnectionStringName)]
public interface IRemittanceServiceDbContext : IEfCoreDbContext
{
    public DbSet<Remittance> Remittances { get; set; }
    public DbSet<RemittanceStatus> RemittanceStatus { get; set; }
}
