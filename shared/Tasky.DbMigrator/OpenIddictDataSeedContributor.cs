using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;

namespace Tasky.DbMigrator;

public class OpenIddictDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly OpenIddictDataSeeder _OpenIddictDataSeeder;
    private readonly IRepository<IdentityRole, Guid> _roleRepository;
    private readonly IGuidGenerator _guidGenerator;


    public OpenIddictDataSeedContributor(OpenIddictDataSeeder OpenIddictDataSeeder
        , IRepository<IdentityRole, Guid> roleRepository
        , IGuidGenerator guidGenerator)
    {
        _OpenIddictDataSeeder = OpenIddictDataSeeder;
        _roleRepository = roleRepository;
        _guidGenerator = guidGenerator;

    }


    public async Task SeedAsync(DataSeedContext context)
    {
        await _OpenIddictDataSeeder.SeedAsync();
        await SeedRolesAsync();
    }

    private async Task SeedRolesAsync()
    {

        if (await _roleRepository.GetCountAsync() > 1)
        {
            return;
        }
        await _roleRepository.InsertAsync(
           new IdentityRole
           (
        _guidGenerator.Create(),
              "Creator"
           ),
           autoSave: true
       );
        await _roleRepository.InsertAsync(
           new IdentityRole
           (
        _guidGenerator.Create(),
              "Supervisor"
           ),
           autoSave: true
       );
        await _roleRepository.InsertAsync(
       new IdentityRole
       (
         _guidGenerator.Create(),
          "Releaser"
       ),
       autoSave: true
   );
        await _roleRepository.InsertAsync(
               new IdentityRole
               (
        _guidGenerator.Create(),
                  "AmlChecker"
               ),
               autoSave: true
           );
    }
}