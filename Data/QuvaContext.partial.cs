using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using QwTest7.Models.Quva;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace QwTest7.Data
{
    public partial class QuvaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(Conf().GetConnectionString("QuvaConnection"),
                b => b.UseOracleSQLCompatibility(Conf()["OracleSQLCompatibility"] ?? "11"));

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