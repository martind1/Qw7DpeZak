using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using QwTest7.Models.Qusy;

namespace QwTest7.Data
{
    public partial class QusyContext : DbContext
    {
        public QusyContext()
        {
        }

        public QusyContext(DbContextOptions<QusyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<QwTest7.Models.Qusy.Filterabfragen>()
              .Property(p => p.ANWE)
              .HasDefaultValueSql("'QUSY'                ");

            builder.Entity<QwTest7.Models.Qusy.Filterabfragen>()
              .Property(p => p.FLTRID)
              .HasPrecision(9);

            builder.Entity<QwTest7.Models.Qusy.Filterabfragen>()
              .Property(p => p.ANZAHLAENDERUNGEN)
              .HasPrecision(9);
        }

        public DbSet<QwTest7.Models.Qusy.Filterabfragen> Filterabfragens { get; set; }
    }
}