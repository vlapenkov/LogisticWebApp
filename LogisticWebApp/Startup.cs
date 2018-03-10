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

namespace LogisticWebApp
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
            

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

          /*  var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser().RequireAssertion(context=>context.User.Identity.)
        .RequireRole("Admin", "SuperUser")
        .Build(); */

            //   services.AddMvc().
            // для того чтобы работал model binding для web api (из 1С передается xml)
            //        AddXmlSerializerFormatters();
            /*
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { new CultureInfo("ru"), };
                options.DefaultRequestCulture = new RequestCulture("ru"); ;
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

            });
            
    */


            services.AddMvc(options =>
            {
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                options.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
            }).AddXmlSerializerFormatters();

            /*    services.AddAuthorization(options =>
                {
                    options.AddPolicy("RequireCarrierId", policy => policy.RequireClaim("CarrierId"));
                }); */

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireCarrierId", policy => policy.Requirements.Add(new CarrierIdRequirement()));
            });

            services.AddScoped<IAuthorizationHandler, CarrierIdHandler>();
            services.AddScoped<CarrierService>();
            services.AddScoped<ClaimService>();
            services.AddScoped<FileUploadService>();

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
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
