using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Logistic.Web.Models;
using Logistic.Data;

namespace LogisticWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<ClaimForTransport> ClaimsForTransport { get; set; }
        public DbSet<ReplyToClaim> RepliesToClaims { get; set; }
        public DbSet<FileModel> Files { get; set; }

        public DbSet<EventLog> EventLogs { get; set; }
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //https://stackoverflow.com/questions/40898365/asp-net-add-migration-composite-primary-key-error-how-to-use-fluent-api
            builder.Entity<ReplyToClaim>()
           .HasKey(c => new { c.GuidOfClaimIn1S, c.CarrierId });
        }

       
    }
}
