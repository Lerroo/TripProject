using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FastTripApp.DAO;
using FastTripApp.DAO.Models.Identity;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.DAO.Repository;
using FastTripApp.BL.Services;
using FastTripApp.BL.Services.Interfaces;
using DinkToPdf.Contracts;
using DinkToPdf;
using System.IO;
using FastTripApp.BL.Services.Util;
using System.Globalization;

namespace UsingIdentity
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
            services.AddEntityFrameworkSqlServer().AddDbContext<UsingIdentityContext>(s => {
                s.UseSqlServer(Configuration.GetConnectionString("UsingIdentityContextConnection"), p => p.MigrationsAssembly(typeof(Startup)
                    .GetTypeInfo().Assembly.GetName().Name))
                    .EnableSensitiveDataLogging();
            });

            services.AddIdentity<UserCustom, IdentityRole>(options => {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
            }).AddEntityFrameworkStores<UsingIdentityContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Identity/Account/Login");

            services.AddHttpContextAccessor();

            services.AddScoped<IRepositoryTrip, RepositoryTrip>();
            services.AddScoped<IRepositoryHistoryTrip, RepositoryHistoryTrip>();
            services.AddScoped<IRepositoryReview, RepositoryReview>();
            services.AddScoped<IRepositoryComment, RepositoryComment>();
            services.AddScoped<IRepositoryUser, RepositoryUser>();
            services.AddScoped<IRepositoryWay, RepositoryWay>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IRepositoryCoords, RepositoryCoords>();
            services.AddScoped<IRepositoryPlace, RepositoryPlace>();

            services.AddScoped<ITripService, TripService>();
            services.AddScoped<IUtilService, UtilService>();
            services.AddScoped<IHistoryTripService, HistoryTripService>();
            services.AddScoped<IUserStatisticService, UserStatisticService>();            
            services.AddTransient<IUserService, UserService>();            

            services.AddTransient<IViewRenderService, ViewRenderService>();
            services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();

            string filePath = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\libwkhtmltox.dll";
            CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(filePath);
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));            

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("UsingIdentityContextConnection")));
            services.AddHangfireServer();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                var cultureInfo = new CultureInfo("en-US");
                cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
                CultureInfo.DefaultThreadCurrentCulture = cultureInfo; 
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHangfireDashboard("/board");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Trip}/{action=Index}");
            endpoints.MapRazorPages();
            });


        }
    }
}
