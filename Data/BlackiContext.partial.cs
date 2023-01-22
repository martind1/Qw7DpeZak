using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using QwTest7.Models.Blacki;
using QwTest7.Services.Kmp;

namespace QwTest7.Data;

public partial class BlackiContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseOracle(BaseUtils.Appsettings().GetConnectionString("QuvaConnection"),
            b => b.UseOracleSQLCompatibility(BaseUtils.Appsettings()["OracleSQLCompatibility"] ?? "11"));

        optionsBuilder.EnableSensitiveDataLogging();

        base.OnConfiguring(optionsBuilder);
    }

}
