#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using Microsoft.EntityFrameworkCore;
using Slice.Core.Underwriter.Data.Models;

namespace Slice.Core.Underwriter.Data
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(@"Host=slice-data-dev.civlfvzpnkzr.us-east-1.rds.amazonaws.com;Port=5432;Username=uw_dev_writer;Password=und4r1TA!-dev-wr;Database=underwriting_dev");
            }
        }

        public DbSet<Warnings> Warnings { get; set; }

        public DbSet<Overrides> Override { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Overrides>(entity =>
            {
                entity.HasKey(e => new { e.Country, e.Area, e.StartsOn, e.EndsOn, e.OverrideType, e.Warning });

                entity.ToTable("override", "weather");

                entity.HasIndex(e => new { e.Country, e.Area}).HasName("override_area_idx");
                entity.HasIndex(e => new { e.Country, e.Area }).HasName("override_country_idx");

                entity.Property(e => e.Country).HasColumnName("country");
                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.StartsOn)
                    .HasColumnName("starts_on")
                    .HasColumnType("date");

                entity.Property(e => e.EndsOn)
                    .HasColumnName("ends_on")
                    .HasColumnType("date");

                entity.Property(e => e.OverrideType).HasColumnName("override_type");
                entity.Property(e => e.Warning).HasColumnName("warning");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("date");

                entity.Property(e => e.RowId)
                    .HasColumnName("row_id")
                    .HasDefaultValueSql("nextval('weather.weather_warnings_row_id_seq'::regclass)");

                entity.Property(e => e.WarningId).HasColumnName("warning_id");
            });


            modelBuilder.Entity<Warnings>(entity =>
            {
                entity.ToTable("warnings", "weather");

                entity.HasIndex(e => new { e.Country, e.Area }).HasName("override_area_idx");
                entity.HasIndex(e => new { e.Country, e.Area }).HasName("override_country_idx");

                entity.Property(e => e.Country).HasColumnName("country");
                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.SearchedOn)
                    .HasColumnName("searched_on")
                    .HasColumnType("date");

                entity.Property(e => e.StartsOn)
                    .HasColumnName("starts_on")
                    .HasColumnType("date");

                entity.Property(e => e.EndsOn)
                    .HasColumnName("ends_on")
                    .HasColumnType("date");

                entity.Property(e => e.Warning).HasColumnName("warning");
                
                entity.Property(e => e.Id)
                    .HasColumnName("row_id")
                    .HasDefaultValueSql("nextval('weather.weather_warnings_row_id_seq'::regclass)");
            });

            modelBuilder.HasSequence("weather_warnings_row_id_seq");
        }
    }
}