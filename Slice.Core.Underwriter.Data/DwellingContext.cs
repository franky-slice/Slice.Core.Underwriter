#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Slice.Core.Underwriter.Data.Models;
using Slice.Core.Underwriter.Data.Models.Rce;

namespace Slice.Core.Underwriter.Data
{
    public class DwellingContext : DbContext
    {
        public DwellingContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<BlacklistedDwellings> BlacklistedDwellings { get; set; }

        public virtual DbSet<DwellingCharacteristics> DwellingCharacteristics { get; set; }

        public virtual DbSet<RceByAdministrativeLevel1> RceByAdministrativeLevel1 { get; set; }

        public virtual DbSet<RceByPostalCode1> RceByPostalCode1 { get; set; }

        #region Events

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis")
                .HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<BlacklistedDwellings>(entity =>
            {
                entity.ToTable("blacklisted_dwellings", "dwelling_info");

                entity.HasIndex(e => e.AdministrativeLevel1).HasName("blacklisted_dwellings_administrative_level_1_idx");

                entity.HasIndex(e => e.CoordPoint)
                    .HasName("blacklisted_dwellings_coord_point_gix")
                    .ForNpgsqlHasMethod("gist");

                entity.HasIndex(e => e.Country).HasName("blacklisted_dwellings_country_idx");
                entity.HasIndex(e => e.HouseNum).HasName("blacklisted_dwellings_house_num_idx");
                entity.HasIndex(e => e.Lat).HasName("blacklisted_dwellings_lat_idx");
                entity.HasIndex(e => e.Lng).HasName("blacklisted_dwellings_lng_idx");
                entity.HasIndex(e => e.PostalCode1).HasName("blacklisted_dwellings_postal_code_1_idx");
                entity.HasIndex(e => e.StreetName).HasName("blacklisted_dwellings_street_name_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v1()");

                entity.Property(e => e.AdministrativeLevel1).HasColumnName("administrative_level_1");
                entity.Property(e => e.AdministrativeLevel2).HasColumnName("administrative_level_2");
                entity.Property(e => e.AptNum).HasColumnName("apt_num");
                entity.Property(e => e.CoordPoint).HasColumnName("coord_point");
                entity.Property(e => e.Country).HasColumnName("country");
                entity.Property(e => e.FormattedAddress).HasColumnName("formatted_address");
                entity.Property(e => e.HouseNum).HasColumnName("house_num");
                entity.Property(e => e.Lat).HasColumnName("lat");
                entity.Property(e => e.Lng).HasColumnName("lng");
                entity.Property(e => e.Locality).HasColumnName("locality");
                entity.Property(e => e.PostalCode1).HasColumnName("postal_code_1");
                entity.Property(e => e.PostalCode2).HasColumnName("postal_code_2");
                entity.Property(e => e.Reason).HasColumnName("reason");
                entity.Property(e => e.StreetName).HasColumnName("street_name");
                entity.Property(e => e.StreetPostdirection).HasColumnName("street_postdirection");
                entity.Property(e => e.StreetPredirection).HasColumnName("street_predirection");

                entity.Property(e => e.StreetPrefix)
                    .HasColumnName("street_prefix")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.StreetPrefixPreposition)
                    .HasColumnName("street_prefix_preposition")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.StreetSuffix).HasColumnName("street_suffix");

                entity.Property(e => e.Tstamp)
                    .HasColumnName("tstamp")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<DwellingCharacteristics>(entity =>
            {
                entity.ToTable("dwelling_characteristics", "dwelling_info");

                entity.HasIndex(e => e.AdministrativeLevel1).HasName("dwelling_characteristics_administrative_level1_idx");

                entity.HasIndex(e => e.CoordPoint)
                    .HasName("dwelling_characteristics_coord_point_gix")
                    .ForNpgsqlHasMethod("gist");

                entity.HasIndex(e => e.Country).HasName("dwelling_characteristics_country_idx");
                entity.HasIndex(e => e.HouseNum).HasName("dwelling_characteristics_house_num_idx");
                entity.HasIndex(e => e.Lat).HasName("dwelling_characteristics_lat_idx");
                entity.HasIndex(e => e.Lng).HasName("dwelling_characteristics_lng_idx");
                entity.HasIndex(e => e.PostalCode1).HasName("dwelling_characteristics_postal_code_1_idx");
                entity.HasIndex(e => e.StreetName).HasName("dwelling_characteristics_street_name_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v1()");

                entity.Property(e => e.AdministrativeLevel1).HasColumnName("administrative_level_1");
                entity.Property(e => e.AdministrativeLevel2).HasColumnName("administrative_level_2");
                entity.Property(e => e.AptNum).HasColumnName("apt_num");
                entity.Property(e => e.CoordPoint).HasColumnName("coord_point");
                entity.Property(e => e.Country).HasColumnName("country");
                entity.Property(e => e.DwellingArea).HasColumnName("dwelling_area");
                entity.Property(e => e.DwellingYearBuilt).HasColumnName("dwelling_year_built");
                entity.Property(e => e.FormattedAddress).HasColumnName("formatted_address");
                entity.Property(e => e.HouseNum).HasColumnName("house_num");
                entity.Property(e => e.Lat).HasColumnName("lat");
                entity.Property(e => e.Lng).HasColumnName("lng");
                entity.Property(e => e.Locality).HasColumnName("locality");
                entity.Property(e => e.PostalCode1).HasColumnName("postal_code_1");
                entity.Property(e => e.PostalCode2).HasColumnName("postal_code_2");
                entity.Property(e => e.StreetName).HasColumnName("street_name");
                entity.Property(e => e.StreetPostdirection).HasColumnName("street_postdirection");
                entity.Property(e => e.StreetPredirection).HasColumnName("street_predirection");

                entity.Property(e => e.StreetPrefix)
                    .HasColumnName("street_prefix")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.StreetPrefixPreposition)
                    .HasColumnName("street_prefix_preposition")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.StreetSuffix).HasColumnName("street_suffix");
                entity.Property(e => e.Tstamp).HasColumnName("tstamp");
            });

            modelBuilder.Entity<RceByAdministrativeLevel1>(entity =>
            {
                entity.ToTable("rce_by_administrative_level_1", "dwelling_rce");

                entity.HasIndex(e => e.AdministrativeLevel1).HasName("rce_by_administrative_level_1_administrative_level_1_idx");
                entity.HasIndex(e => e.Country).HasName("rce_by_administrative_level_1_country_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v1()");

                entity.Property(e => e.AdministrativeLevel1).HasColumnName("administrative_level_1");
                entity.Property(e => e.Country).HasColumnName("country");
                entity.Property(e => e.CurrencyUnits).HasColumnName("currency_units");
                entity.Property(e => e.DefaultRce).HasColumnName("default_rce");
            });

            modelBuilder.Entity<RceByPostalCode1>(entity =>
            {
                entity.ToTable("rce_by_postal_code_1", "dwelling_rce");

                entity.HasIndex(e => e.Country).HasName("rce_by_postal_code_1_country_idx");
                entity.HasIndex(e => e.PostalCode1).HasName("rce_by_postal_code_1_postal_code_1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v1()");

                entity.Property(e => e.Country).HasColumnName("country");
                entity.Property(e => e.CurrencyUnits).HasColumnName("currency_units");
                entity.Property(e => e.DefaultRce).HasColumnName("default_rce");
                entity.Property(e => e.PostalCode1).HasColumnName("postal_code_1");
            });
        }

        public override int SaveChanges()
        {
            var changes = ChangeTracker.Entries<IRceBaseModel>();
            var changeList = changes.ToList();

            foreach (var entry in changeList)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Id = Guid.NewGuid();
                        break;
                    case EntityState.Modified:
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return base.SaveChanges();
        }

        #endregion 
    }
}