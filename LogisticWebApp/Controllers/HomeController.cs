using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logistic.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using LogisticWebApp.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using Logistic.Data;
using Logistic.Web.Services;
using ServiceReference1;
using static ServiceReference1.ServiceCarrierPortTypeClient;
using System.Net;
using Microsoft.AspNetCore.Localization;

namespace Logistic.Web.Controllers
{
   /// <summary>
   /// Класс не тспользуется
   /// </summary>
    public class HomeController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        ApplicationDbContext _dbContext;
        IHostingEnvironment _appEnvironment;
        CarrierService _carrierService;


        public HomeController(UserManager<ApplicationUser> userManager, 
            ApplicationDbContext dbContext,
        IHostingEnvironment appEnvironment,
        CarrierService carrierService    
        )
        
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _appEnvironment = appEnvironment;
            _carrierService = carrierService;

        }

        public IActionResult GetCulture()
        {

           var culture= Request.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture;
            
            return View(culture);
        }

        public IActionResult Messaging()
        {
            return View();
        }


        public async Task<IActionResult> Index()
        {
           
        


            ViewBag.CarrierId = _carrierService.Carrier?.Inn;
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                var roles = await _userManager.GetRolesAsync(user);
                ViewBag.Roles = String.Join(";", roles);
            }
            
            
            return View();
        }

        [Authorize(Roles ="Administrator")]
        [HttpGet]
        public async Task<IActionResult> AddFile()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
       public async Task<IActionResult> AddFile(IFormFileCollection uploads)
        {
            foreach (var uploadedFile in uploads)
            {
                
                
                // путь к папке Files
                string path = "/Files/" +Guid.NewGuid().ToString()+ Path.GetExtension(uploadedFile.FileName);
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
                _dbContext.Files.Add(file);
            }
            _dbContext.SaveChanges();

            return RedirectToAction("AddFile");
        }

        
        public string Env()
        {
           return  _appEnvironment.EnvironmentName;
        }

        // [Authorize(Roles ="Manager")] 
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
