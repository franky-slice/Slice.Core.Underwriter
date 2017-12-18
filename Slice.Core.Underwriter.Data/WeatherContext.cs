#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using Microsoft.EntityFrameworkCore;
using Slice.Core.Underwriter.Data.Extensions;
using Slice.Core.Underwriter.Data.Models.Weather;

namespace Slice.Core.Underwriter.Data
{
    public class WeatherContext : UnderwriterContext
    {
        public WeatherContext(DbContextOptions options) : base(options)
        {
        }

        #region Events

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("weather");

            base.OnModelCreating(builder);
        }

        #endregion

        #region Models

        public DbSet<Warning> Warnings { get; set; }

        public DbSet<Override> Override { get; set; }

        #endregion
    }
}