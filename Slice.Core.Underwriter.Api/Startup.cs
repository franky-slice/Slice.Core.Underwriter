#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Slice.Core.Underwriter.Api.Filters;
using Slice.Core.Underwriter.Data;
using Slice.Core.Underwriter.Data.Interfaces;
using Slice.Core.Underwriter.Risk.Services;
using Slice.Core.Underwriter.Weather.Managers;
using Swashbuckle.AspNetCore.Swagger;

namespace Slice.Core.Underwriter.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDataContext(services);

            ConfigureDependencies(services);

            services.AddMvc(c => {
                c.Filters.Add(typeof(CustomExceptionFilterAttribute));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Slice.Core.Underwriter", Version = "v1" });
            });
        }

        protected void ConfigureDataContext(IServiceCollection services)
        {
            var weatherConnectionString = Configuration.GetConnectionString("WeatherContext");
            services.AddEntityFrameworkNpgsql().AddDbContext<WeatherContext>(
                options => options.UseNpgsql(weatherConnectionString,
                    x => x.MigrationsHistoryTable("__MyMigrationsHistory", "weather")));


            var dwellingConnectionString = Configuration.GetConnectionString("DwellingContect");
            services.AddEntityFrameworkNpgsql().AddDbContext<DwellingContext>(
                options => options.UseNpgsql(dwellingConnectionString));
        }

        protected void ConfigureDependencies(IServiceCollection services)
        {
            services.AddSingleton<ILoggerFactory, LoggerFactory>();

            services.AddScoped(typeof(IWeatherRepository<>), typeof(WeatherRepository<>));

            // Weather Management
            services.AddScoped<IWeatherOverrideManager, WeatherOverrideManager>();
            services.AddScoped<IWeatherWarningManager, WeatherWarningManager>();

            // Risk Management
            services.AddScoped<IRiskService, RiskService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Register Swagger middleware
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Slice.Core.Underwriter V1");
                });
            }

            ConfigureExceptionHandling(app, env);

            // Register MVC middleware
            app.UseMvc();
        }

        private void ConfigureExceptionHandling(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Global middleware exception handling
            app.UseExceptionHandler(
                options => {
                    options.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.ContentType = "text/html";
                            var ex = context.Features.Get<IExceptionHandlerFeature>();
                            if (ex != null)
                            {
                                var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace }";
                                await context.Response.WriteAsync(err).ConfigureAwait(false);
                            }
                        });
                }
            );
        }
    }
}