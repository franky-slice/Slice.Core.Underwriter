#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Slice.Core.Underwriter.Data.Extensions;
using Slice.Core.Underwriter.Data.Interfaces;
using Slice.Core.Underwriter.Data.Models.Weather;

namespace Slice.Core.Underwriter.Data
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
        {
        }

        #region Events

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("weather");

            base.OnModelCreating(builder);

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                // Replace table names
                entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();

                // Replace column names            
                foreach (var property in entity.GetProperties())
                {
                    property.Relational().ColumnName = property.Name.ToSnakeCase();
                }

                foreach (var key in entity.GetKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name.ToSnakeCase();
                }
            }
        }

        public override int SaveChanges()
        {
            var currentDate = DateTime.UtcNow;

            // We should never detect changes because it adds into db context all models loaded from the cache.
            // Instead we manually set State.Modified when updating a modal in the repository.
            // ChangeTracker.DetectChanges();

            var changes = ChangeTracker.Entries<IBaseModel>();
            var changeList = changes.ToList();

            foreach (var entry in changeList)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = currentDate;
                        entry.Entity.ModifiedOn = currentDate;
                        entry.Entity.Id = Guid.NewGuid();
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedOn = currentDate;
                        // Make sure the DateCreated is never modified
                        entry.Entity.CreatedOn = entry.OriginalValues.GetValue<DateTime>("DateCreated");
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

        #region Models

        public DbSet<Warning> Warnings { get; set; }

        public DbSet<Override> Override { get; set; }

        #endregion
    }
}