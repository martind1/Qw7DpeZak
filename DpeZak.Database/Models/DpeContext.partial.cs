using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DpeZak.Database.Models;

public partial class DpeContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Conf().GetConnectionString("DefaultConnection"));

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