using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Logistic.Web.Models;
using LogisticWebApp.Models.ManageViewModels;
using LogisticWebApp.Services;
using LogisticWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Logistic.Web.Services;
using AutoMapper;

namespace Logistic.Web.Controllers
{
   // [Authorize(Roles ="Manager")]

    public class ManagerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;
        private readonly ApplicationDbContext _dbContext;
        private readonly CarrierService _carrierService;
        private readonly IMapper _mapper;



        public ManagerController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IEmailSender emailSender,
          ILogger<ManageController> logger,
          UrlEncoder urlEncoder,
          ApplicationDbContext dbContext,
          CarrierService carrierService,
          IMapper mapper
          )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _urlEncoder = urlEncoder;
            _dbContext = dbContext;
            _carrierService = carrierService;
            _mapper = mapper;
        }

       
        /*
       */

        private  bool CheckIfCarrierExistsInAnotherUser(string userId,string carrierId) {
          return  _dbContext.Users.Any(p => p.Id != userId && p.CarrierId == carrierId);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return View( await _dbContext.Users.Include(p=>p.Carrier).ToListAsync());
        }

        private async  Task<ApplicationUserVm> GetModel(string userId)
        {
            ApplicationUserVm model;
            var user = await _carrierService.GetUserById(userId);

            var carrierId = user.CarrierId;

            model = _mapper.Map<ApplicationUserVm>(user);
          //  model = new ApplicationUserVm(userId, user.UserName, user.Fio, user.Inn, user.Kpp, user.CarrierId);
                   

            model.Carriers = await _dbContext.Carriers.Where(p=>p.IsActive).OrderBy(c => c.FullName).ToListAsync();

            if (carrierId != null)
            {
                model.Cars = await _carrierService.GetCars(carrierId);
                model.Drivers = await _carrierService.GetDrivers(carrierId);
            }
            
            return model;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {

           var model= await GetModel(userId);

         return View (model);
                        
        }


       





        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUserVm userUpdated)
        {

            if (userUpdated.CarrierId != null && CheckIfCarrierExistsInAnotherUser(userUpdated.Id, userUpdated.CarrierId))
                ModelState.AddModelError("CarrierId", "Среди пользователей уже есть такой перевозчик");

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _carrierService.GetUserById(userUpdated.Id);
                    user.Inn = userUpdated.Inn;
                    user.Kpp = userUpdated.Kpp;
                    user.Fio = userUpdated.Fio;
                    user.CarrierId = userUpdated.CarrierId;

              //      await AddClaim(userUpdated.Id, "CarrierId", userUpdated.CarrierId);
                                        
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    _logger.LogError(e.Message);
                       throw;
                    
                }
                return RedirectToAction(nameof(Index));
            }


            return View(await GetModel(userUpdated.Id));

        }

       
    }
}
