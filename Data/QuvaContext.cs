using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using QwTest7.Models.Quva;

namespace QwTest7.Data
{
    public partial class QuvaContext : DbContext
    {
        public QuvaContext()
        {
        }

        public QuvaContext(DbContextOptions<QuvaContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<QwTest7.Models.Quva.Fahrzeuge>()
              .HasOne(i => i.Speditionen)
              .WithMany(i => i.Fahrzeuges)
              .HasForeignKey(i => i.SPEDID)
              .HasPrincipalKey(i => i.SPEDID);

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .HasOne(i => i.Fahrzeuge)
              .WithMany(i => i.Kartens)
              .HasForeignKey(i => i.FRZGID)
              .HasPrincipalKey(i => i.FRZGID);

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .HasOne(i => i.Speditionen)
              .WithMany(i => i.Kartens)
              .HasForeignKey(i => i.SPEDID)
              .HasPrincipalKey(i => i.SPEDID);

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .Property(p => p.KUNDKNZ)
              .HasDefaultValueSql("'J'");

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .Property(p => p.SPERRKNZ)
              .HasDefaultValueSql("'N'");

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .Property(p => p.SPENDERKNZ)
              .HasDefaultValueSql("'N'");

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .Property(p => p.SORTEKNZ)
              .HasDefaultValueSql("'N'");

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .Property(p => p.FREMDWAEGUNG)
              .HasDefaultValueSql("'N'");

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .Property(p => p.PROBENPFLICHT)
              .HasDefaultValueSql("'J'");

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .Property(p => p.EINGANGKNZ)
              .HasDefaultValueSql("'N'");

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .Property(p => p.KOMBIKNZ)
              .HasDefaultValueSql("'N'");

            builder.Entity<QwTest7.Models.Quva.Speditionen>()
              .Property(p => p.PALETTENKONTO)
              .HasDefaultValueSql("'N'");

            builder.Entity<QwTest7.Models.Quva.Fahrzeuge>()
              .Property(p => p.FRZGID)
              .HasPrecision(9);

            builder.Entity<QwTest7.Models.Quva.Fahrzeuge>()
              .Property(p => p.ANZAHLAENDERUNGEN)
              .HasPrecision(9);

            builder.Entity<QwTest7.Models.Quva.Fahrzeuge>()
              .Property(p => p.TARAFREIANZAHL)
              .HasPrecision(5);

            builder.Entity<QwTest7.Models.Quva.Fahrzeuge>()
              .Property(p => p.SPEDID)
              .HasPrecision(9);

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .Property(p => p.KARTID)
              .HasPrecision(9);

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .Property(p => p.ANZAHLAENDERUNGEN)
              .HasPrecision(9);

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .Property(p => p.PEKRID)
              .HasPrecision(9);

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .Property(p => p.SILONR)
              .HasPrecision(3);

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .Property(p => p.TARATAGE)
              .HasPrecision(5);

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .Property(p => p.SPEDID)
              .HasPrecision(9);

            builder.Entity<QwTest7.Models.Quva.Karten>()
              .Property(p => p.FRZGID)
              .HasPrecision(9);

            builder.Entity<QwTest7.Models.Quva.Speditionen>()
              .Property(p => p.SPEDID)
              .HasPrecision(9);

            builder.Entity<QwTest7.Models.Quva.Speditionen>()
              .Property(p => p.ANZAHLAENDERUNGEN)
              .HasPrecision(9);

            builder.Entity<QwTest7.Models.Quva.Speditionen>()
              .Property(p => p.PALINVBSTE)
              .HasPrecision(6);

            builder.Entity<QwTest7.Models.Quva.Speditionen>()
              .Property(p => p.PALINVBSTD)
              .HasPrecision(6);
        }

        public DbSet<QwTest7.Models.Quva.Fahrzeuge> Fahrzeuges { get; set; }

        public DbSet<QwTest7.Models.Quva.Karten> Kartens { get; set; }

        public DbSet<QwTest7.Models.Quva.Speditionen> Speditionens { get; set; }
    }
}