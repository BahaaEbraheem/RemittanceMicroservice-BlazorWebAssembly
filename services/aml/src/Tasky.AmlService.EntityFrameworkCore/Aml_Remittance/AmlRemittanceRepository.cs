using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasky.AmlService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.Domain.Services;
using Volo.Abp.EntityFrameworkCore;

namespace Tasky.AmlService.Aml_Remittance
{
    public class AmlRemittanceRepository : EfCoreRepository<AmlServiceDbContext, AmlRemittance, Guid>, IAmlRemittanceRepository
    {
        public AmlRemittanceRepository(
            IDbContextProvider<AmlServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

    }
}