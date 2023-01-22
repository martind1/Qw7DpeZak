using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using QwTest7.Models;
using System.Reflection.Emit;
using QwTest7.Services.Kmp;

namespace QwTest7.Data
{
    public partial class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(BaseUtils.Appsettings().GetConnectionString("QuvaConnection"),
                b => b.UseOracleSQLCompatibility(BaseUtils.Appsettings()["OracleSQLCompatibility"] ?? "11"));

            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

    }
}