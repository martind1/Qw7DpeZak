using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using QwTest7.Models.Blacki;

namespace QwTest7.Data;

public partial class KmpDbContext : DbContext
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

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        #region Bulk configuration via model class for all table names
        foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
        {
            //MD nicht nötig weil über Template T4 Anpassung gelöst
            // All table names = class names (~ EF 6.x), 
            // except the classes that have a [Table] annotation or derived classes (where ToTable() is not allowed in EF Core >= 3.0)
            var annotation = entity.ClrType?.GetCustomAttribute<TableAttribute>();
            if (annotation == null && entity.BaseType == null)
            {
                //entity.Relational().TableName = entity.DisplayName();
            }
        }
        #endregion
    }
}
