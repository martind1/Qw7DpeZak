using DpeZak.Database.Authentification.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DpeZak.Database.Migrate
{
    public partial class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
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
}