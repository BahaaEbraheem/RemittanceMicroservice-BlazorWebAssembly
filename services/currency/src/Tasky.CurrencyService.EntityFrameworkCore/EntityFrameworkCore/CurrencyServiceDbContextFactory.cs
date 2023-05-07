using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tasky.CurrencyService.EntityFrameworkCore;
using Tasky.CurrencyService;
namespace Tasky.CurrencyService.EntityFrameworkCore;

public class CurrencyServiceDbContextFactory : IDesignTimeDbContextFactory<CurrencyServiceDbContext>
{
    public CurrencyServiceDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<CurrencyServiceDbContext>()
            .UseSqlServer(GetConnectionStringFromConfiguration());

        return new CurrencyServiceDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration()
            .GetConnectionString(CurrencyServiceDbProperties.ConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(
                Path.Combine(
                    Directory.GetParent(Directory.GetCurrentDirectory())?.Parent!.FullName!,
                    $"host{Path.DirectorySeparatorChar}Tasky.CurrencyService.HttpApi.Host"
                )
            )
            .AddJsonFile("appsettings.json", false);

        return builder.Build();
    }
}
