using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace QwTest7.Database.Models;

public partial class QuvaContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseOracle(Conf().GetConnectionString("QuvaConnection"),
            b => b.UseOracleSQLCompatibility(Conf()["OracleSQLCompatibility"] ?? "11"));

        optionsBuilder.EnableSensitiveDataLogging();

        base.OnConfiguring(optionsBuilder);
    }

    private static IConfiguration Conf()
    {
        var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json",
            optional: true, reloadOnChange: false);
        return configurationBuilder.Build();
    }

}