using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using QwTest7.Models;
using System.Reflection.Emit;

namespace QwTest7.Data
{
    public partial class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(GetConnection().GetConnectionString("QuvaConnection"),
                b => b.UseOracleSQLCompatibility(GetConnection()["OracleSQLCompatibility"] ?? "11"));

            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        private static IConfiguration GetConnection()
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json",
                optional: true, reloadOnChange: false);
            return configurationBuilder.Build();
        }

        partial void OnModelBuilding(ModelBuilder builder)
        {
            //Kompatibilität mit Ora 11g
            //builder.Model.SetMaxIdentifierLength(30);
        }
    }
}