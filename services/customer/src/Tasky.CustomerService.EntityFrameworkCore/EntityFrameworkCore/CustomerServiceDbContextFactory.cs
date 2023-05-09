using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tasky.CustomerService.EntityFrameworkCore;
using Tasky.CustomerService;
namespace Tasky.CustomerService.EntityFrameworkCore;

public class CustomerServiceDbContextFactory : IDesignTimeDbContextFactory<CustomerServiceDbContext>
{
    public CustomerServiceDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<CustomerServiceDbContext>()
            .UseSqlServer(GetConnectionStringFromConfiguration());

        return new CustomerServiceDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration()
            .GetConnectionString(CustomerServiceDbProperties.ConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(
                Path.Combine(
                    Directory.GetParent(Directory.GetCurrentDirectory())?.Parent!.FullName!,
                    $"host{Path.DirectorySeparatorChar}Tasky.CustomerService.HttpApi.Host"
                )
            )
            .AddJsonFile("appsettings.json", false);

        return builder.Build();
    }
}
