using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Logistic.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Logistic.Web.Controllers
{
    public class UserSettingsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSettingsController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async  Task<IActionResult> SendToken(string token)
        {
            Claim claim = new Claim("token", token);
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                var claimfound = (await _userManager.GetClaimsAsync(user)).FirstOrDefault(p => p.Type == "token");

                if (claimfound != null) await _userManager.RemoveClaimAsync(user, claimfound);

                await _userManager.AddClaimAsync(user, claim);

            }

            return Ok();
        }
    }
}