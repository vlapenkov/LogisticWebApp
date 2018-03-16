using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Logistic.Data;
using LogisticWebApp.Data;
using Logistic.Models;
using X.PagedList;
using Microsoft.AspNetCore.Localization;
using Logistic.Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using Logistic.Web.Services;
using Microsoft.Extensions.Logging;
using ServiceReference1;
using static ServiceReference1.ServiceCarrierPortTypeClient;

namespace Logistic.Web.Controllers
{
    /// <summary>
    /// Данный класс для того чтобы пользователь мог видеть и отвечать на заявки
    /// </summary>
    [Authorize(Policy = "RequireCarrierId")]
    
    
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CarrierService _carrierService;
        private readonly ClaimService _claimService;
        private readonly ILogger<ClaimsController> _logger;

        public ClaimsController(ApplicationDbContext context, CarrierService carrierService, ILogger<ClaimsController> logger, ClaimService claimService)
        {
            _context = context;
            _carrierService = carrierService;
            _logger = logger;
            _claimService = claimService;
        }


        //[ResponseCache(Duration = 100, VaryByQueryKeys = new[] { "page", "searchstring" })]
        public async Task<IActionResult> Index(int? page, string searchstring, FilterByStatus FilterByStatus, VolumeRanges? Volume, [ModelBinder(typeof(DateTimeModelBinder))] DateTime? startDate, [ModelBinder(typeof(DateTimeModelBinder))] DateTime? endDate)
        {
          
            int pageSize = 3;
            var carrierId = _carrierService.Carrier.Id;
            var model = new ClaimsViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                Cars = await _carrierService.GetCars(carrierId),
                Drivers = await _carrierService.GetDrivers(carrierId),
                CarrierId = carrierId,
                SearchString = searchstring,
                FilterByStatus = FilterByStatus,
                Volume = Volume                 
            };

           var claimsResult =await _claimService.GetFilteredClaimsAsync(model);
            model.Claims = claimsResult.ToPagedList(page ?? 1, pageSize);
          

        
            return View( model);

            
        }

        private IActionResult ReturnModelErrorsAsJson()
        {
            var errorList = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );
            return Json(
           new
           { Success = false, Element = errorList.First().Key, Text = errorList.First().Value[0] });
        }

        private void CheckModel(ModelStateDictionary modelState, ResponseViewModel model)
        {
            if (model.ArrivalDate >= model.UnloadDate)   modelState.AddModelError("UnloadDate", "дата разгрузки не может быть ранее даты подачи авто");
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateResponse(ResponseViewModel model, [ModelBinder(typeof(DateTimeModelBinder))] DateTime? ArrivalDate, 
            [ModelBinder(typeof(DateTimeModelBinder))] DateTime? UnloadDate)
        {
            model.ArrivalDate = ArrivalDate;
            model.UnloadDate = UnloadDate;

            CheckModel(ModelState , model);

            if (!ModelState.IsValid) return ReturnModelErrorsAsJson();

            
                try
                {
                
                var proxy = new ServiceCarrierPortTypeClient(EndpointConfiguration.ServiceCarrierSoap);

                Driver driver = await _context.Drivers.FindAsync((int)model.DriverId);
                Car car = await _context.Cars.FindAsync((int)model.CarId);

                var result = await proxy.CarrierAnswerAsync(_carrierService.Carrier.Id, model.GuidOfClaimIn1S.ToString(), (DateTime)ArrivalDate, (DateTime)UnloadDate, (int) model.Cost, driver.Fio, car.ToString());
                //return Ok(result.Body.@return);

                var reply = new ReplyToClaim
                    {
                        GuidOfClaimIn1S = model.GuidOfClaimIn1S,
                        CarrierId = _carrierService.Carrier.Id,
                        ArrivalDate = ArrivalDate,
                        UnloadDate = UnloadDate,
                        Cost = model.Cost,
                        DriverId = model.DriverId,
                        CarId = model.CarId
                        
                        //   NdsState = model.NdsState

                    };
                    _context.RepliesToClaims.Add(reply);

                    await _context.SaveChangesAsync();

                    return PartialView(reply);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw;
                }                

            }
         
            
          
            
        
        
    }
}
