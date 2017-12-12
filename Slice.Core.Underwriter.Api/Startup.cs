#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slice.Core.Underwriter.Data;
using Slice.Core.Underwriter.Data.Interfaces;
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
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Slice.Core.Underwriter", Version = "v1" });
            });


            ConfigureDataContext(services);

            ConfigureDependencies(services);
        }

        protected void ConfigureDataContext(IServiceCollection services)
        {
            var weatherConnectionString = Configuration.GetConnectionString("WeatherContext");
            services.AddEntityFrameworkNpgsql().AddDbContext<WeatherContext>(options => options.UseNpgsql(weatherConnectionString));
        }

        protected void ConfigureDependencies(IServiceCollection services)
        {
            services.AddScoped(typeof(IWeatherRepository<>), typeof(WeatherRepository<>));

            services.AddScoped<IWeatherOverrideManager, WeatherOverrideManager>();
            services.AddScoped<IWeatherWarningManager, WeatherWarningManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();

                // Register Swagger middleware
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Slice.Core.Underwriter V1");
                });
            }

            // Register MVC middleware
            app.UseMvc();
        }
    }
}