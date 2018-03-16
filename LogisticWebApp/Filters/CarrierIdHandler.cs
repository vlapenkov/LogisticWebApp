using Logistic.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistic.Web.Filters
{

    public class CarrierIdRequirement : IAuthorizationRequirement
    {
        
    }

    public class CarrierIdHandler : AuthorizationHandler<CarrierIdRequirement>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public CarrierIdHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       CarrierIdRequirement requirement)
        {
            var user =  _userManager.GetUserAsync(context.User).Result;
            if (user?.CarrierId!=null) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
