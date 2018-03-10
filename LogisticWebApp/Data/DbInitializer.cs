using LogisticWebApp.Data;
using LogisticWebApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Logistic.Web.Models;

namespace Logistic.Web.Data
{
    public static class DbInitializer
    {
        public static  void SeedRoles(ApplicationDbContext dbContext)
        {
            //I'm bombing here
            //ApplicationDbContext context = applicationBuilder.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            //  Debug.Write(dbContext.Roles.Count());

            dbContext.Database.EnsureCreated();
            

            //If there is already an Administrator role, abort
            if (!dbContext.Roles.Any(r => r.Name == "Administrator")) dbContext.Roles.Add(new IdentityRole("Administrator"));

            if (!dbContext.Roles.Any(r => r.Name == "Manager")) dbContext.Roles.Add(new IdentityRole("Manager"));

          
            dbContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        public static   void SeedUsers( UserManager<ApplicationUser> userManager )
        {

          string  password = "facility", email = "vovan@yst.ru";

            if (userManager.FindByNameAsync(email).Result==null) {
                var user = new ApplicationUser { UserName = email, Email = email };
                 userManager.CreateAsync(user, password).Wait();
            }
           
           

        }


       

    }
}
