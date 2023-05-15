using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Tasky.RemittanceService.Remittances
{
    public interface IRemittanceRepository : IRepository<Remittance, Guid>, IDomainService
    {
        Task<Remittance> FindBySerialNumAsync(string serialNum);
        Task<Remittance> FindRemittance_StillDraftAsync(double amount, string receiverName);
        Task<List<Remittance>> GetAllAsync();
        Task<List<Remittance>> GetListRemittancesStatusAsync(int skipCount, int maxResultCount, string sorting, Remittance filter);
        Task<int> GetTotalCountAsync(Remittance filter);
        Task<bool> IsApprovedRemittanceAsync(Remittance remittance);

    }

}