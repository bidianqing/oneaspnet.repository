﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneAspNet.Repository.EntityFramework;

namespace EntityFrameworkSample
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
            services.AddControllersWithViews();

            services.AddScoped(typeof(IEfRepository<>), typeof(SampleRepository<>));
            services.AddDbContextPool<SampleDbContext>(options =>
            {
                // Install >> Pomelo.EntityFrameworkCore.MySql
                options.UseMySql(Configuration.GetConnectionString("MySqlConnectionString"));

                // Install >> Microsoft.EntityFrameworkCore.SqlServer
                //options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnectionString"));
            });

            services.AddDbContextFactory<SampleDbContext>(options =>
            {
                // Install >> Pomelo.EntityFrameworkCore.MySql
                options.UseMySql(Configuration.GetConnectionString("MySqlConnectionString"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
