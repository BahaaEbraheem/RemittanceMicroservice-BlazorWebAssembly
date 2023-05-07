using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Tasky.CurrencyService.EntityFrameworkCore;

public class CurrencyServiceHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<CurrencyServiceHttpApiHostMigrationsDbContext>
{
    public CurrencyServiceHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<CurrencyServiceHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("CurrencyService"));

        return new CurrencyServiceHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
