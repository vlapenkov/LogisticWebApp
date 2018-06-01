using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LogisticWebApp.Data;
using LogisticWebApp.Models;
using LogisticWebApp.Services;
using Logistic.Web.Data;
using Logistic.Web.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Logistic.Web.Filters;
using Logistic.Web.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using AutoMapper;

namespace LogisticWebApp
{
    public class Startup
    {
        /*
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }*/
        public Startup(IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);


         

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(
                options => {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    
                }
                )
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            
            services.ConfigureApplicationCookie(options =>
            {              
                options.AccessDeniedPath = "/Account/Login";              
            });


            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                 
                    new CultureInfo("ru")
                };

                options.DefaultRequestCulture = new RequestCulture("ru");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

           

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc(options =>
            {
               // options.Filters.Add(new RequireHttpsAttribute());
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                options.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
            }).AddXmlSerializerFormatters()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            services.AddAutoMapper();

            /*    services.AddAuthorization(options =>
                {
                    options.AddPolicy("RequireCarrierId", policy => policy.RequireClaim("CarrierId"));
                }); */

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireCarrierId", policy => policy.Requirements.Add(new CarrierIdRequirement()));
            });


            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<IAuthorizationHandler, CarrierIdHandler>();
            services.AddScoped<CarrierService>();
            services.AddScoped<ClaimService>();
            services.AddScoped<FileUploadService>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<FcmService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext, 
            UserManager<ApplicationUser> userManager, ILoggerFactory loggerFactory)
        {

          
            loggerFactory.AddDebug();
            loggerFactory.AddDebug(LogLevel.Debug);
            loggerFactory.AddDebug(LogLevel.Error);
            

            loggerFactory.AddProvider(new DbLoggerProvider((_, b) => b == LogLevel.Debug || b == LogLevel.Error, Configuration.GetConnectionString("DefaultConnection")));
            //loggerFactory.AddContext((_,b)=>b==LogLevel.Debug||b==LogLevel.Error );


            // редирект на Https
         /*   var options = new RewriteOptions().AddRedirectToHttps();

            app.UseRewriter(options); */

            // локазизация (partial views не работали)
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

           // DbInitializer.SeedUsers(userManager);
            app.UseStaticFiles();


          
            app.UseAuthentication();

          //  app.UseRequestLocalization();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Claims}/{action=Index}/{id?}");
            });
        }
    }
}
