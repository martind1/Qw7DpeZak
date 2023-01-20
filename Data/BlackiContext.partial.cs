using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using QwTest7.Models.Blacki;

namespace QwTest7.Data;

public partial class BlackiContext : DbContext
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
