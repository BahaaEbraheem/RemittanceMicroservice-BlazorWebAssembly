using Microsoft.EntityFrameworkCore;
using Tasky.AmlService.Aml_Person;
using Tasky.AmlService.Aml_Remittance;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tasky.AmlService.EntityFrameworkCore;

[ConnectionStringName(AmlServiceDbProperties.ConnectionStringName)]
public interface IAmlServiceDbContext : IEfCoreDbContext
{
    public DbSet<AmlRemittance> AmlRemittances { get; set; }
    public DbSet<AmlPerson> AmlPersons { get; set; }
}
