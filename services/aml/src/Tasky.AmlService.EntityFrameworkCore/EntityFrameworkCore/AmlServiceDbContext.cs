using Microsoft.EntityFrameworkCore;
using Tasky.AmlService.Aml_Person;
using Tasky.AmlService.Aml_Remittance;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DistributedEvents;

namespace Tasky.AmlService.EntityFrameworkCore;

[ConnectionStringName(AmlServiceDbProperties.ConnectionStringName)]
public class AmlServiceDbContext : AbpDbContext<AmlServiceDbContext>, IAmlServiceDbContext, IHasEventOutbox, IHasEventInbox
{

    public DbSet<AmlRemittance> AmlRemittances { get; set; }
    public DbSet<AmlPerson> AmlPersons { get; set; }
    public DbSet<OutgoingEventRecord> OutgoingEvents { get; set ; }
    public DbSet<IncomingEventRecord> IncomingEvents { get ; set ; }

    public AmlServiceDbContext(DbContextOptions<AmlServiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureAmlService();
        builder.ConfigureEventOutbox();

    }
}
