using Airport.Business.Interfaces;
using Airport.Business.Services;
using Airport.Data.Repositories;
using AirportSimolator.DataContext;
using AirportSimolator.Hubs;
using AirportSimolator.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AirportSimolator
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
            
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(conf => {
                conf.KeepAliveInterval = TimeSpan.FromSeconds(5);
                conf.ClientTimeoutInterval = TimeSpan.FromMinutes(2);
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://localhost:3000")
                    .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });

            services.AddDbContext<AirportContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")) , ServiceLifetime.Singleton);

            services.AddSingleton<IAirportRepository, AirportRepository>();

            services.AddSingleton<IHubService , HubService>();

            

            services.AddSingleton<IAirportLogic, AirportLogic>();
            services.AddSingleton<IPlanesService,PlanesService>();
            services.AddSingleton<IStationService, StationService>();

            services.AddControllersWithViews();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , AirportContext Context)
        {
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseCors();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<AirportClientHub>("/airportclienthub");
                endpoints.MapHub<AirportServerHub>("/airportserverhub");
            });
        }
    }
}
