using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logistic.Web.Models;
using Logistic.Web.Services;
using LogisticWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Logistic.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Logistic.Web.Controllers
{
    /// <summary>
    /// Данный класс служит для управления машинами водителями, но только после того как 
    /// пользователю присвоен carrierId
    /// </summary>
    
    public class CarsAndDriversController : Controller
    {
        private readonly CarrierService _carrierService;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CarsAndDriversController> _logger;
        private readonly FileUploadService _fileUploadService;

        public CarsAndDriversController(CarrierService carrierService, ApplicationDbContext dbContext, ILogger<CarsAndDriversController> logger, FileUploadService fileUploadService)
        {
            this._carrierService = carrierService;
            this._dbContext = dbContext;
            _logger = logger;
            _fileUploadService = fileUploadService;

        }

        public async Task<IActionResult> Index()
        {
            var carrierId = _carrierService.Carrier.Id;

            var model = new CarsAndDriversViewModel
            {
                Carrier = _carrierService.Carrier,
                Cars = await _carrierService.GetCars(carrierId),
                Drivers = await _carrierService.GetDrivers(carrierId)
            }
            ;


            return View(model);
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

        [HttpPost]
        public async Task<IActionResult> AddCar(CarVm model, IFormFile carfile)
        {
            string filePath = null;
            if (carfile != null)
                filePath = await _fileUploadService.AddFile(carfile);


            if (!ModelState.IsValid) return ReturnModelErrorsAsJson();


            var car = new Car
            {
                CarrierId = _carrierService.Carrier.Id,
                Brand = model.Brand,
                Model = model.CarModel,
                StateNumber = model.StateNumber,
                Volume = model.Volume,
                IsActive = true,
                FilePath = filePath

            };
            _dbContext.Cars.Add(car);
            await _dbContext.SaveChangesAsync();

            return PartialView("_Car",car);

        }

        [HttpPost]
        public async Task<IActionResult> AddDriver(DriverVm model, IFormFile driverfile)
        {

            string filePath = null;
            if (driverfile != null)
                filePath = await _fileUploadService.AddFile(driverfile);

            if (!ModelState.IsValid) return ReturnModelErrorsAsJson();

            var driver = new Driver
            {
                CarrierId = _carrierService.Carrier.Id,
                Fio = model.Fio,
                PhoneNumber = model.PhoneNumber,
                IsActive = true,
                HasContract = model.HasContract,
                FilePath= filePath

            };

            _dbContext.Drivers.Add(driver);
            await _dbContext.SaveChangesAsync();

            return PartialView("_Driver",driver);
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteCar(int carId )
        {

            var carrierId = _carrierService.Carrier.Id;
           var car= _dbContext.Cars.FirstOrDefault(p => p.CarrierId == carrierId && p.Id == carId);

            if (car==null) return Json ( new { Success = false, Message = "Нет такого авто" });

               if( _dbContext.RepliesToClaims.Any(r=>r.CarId== carId))
                return Json(new { Success = false, Message = "Авто уже используется в документах" });

            _dbContext.Cars.Remove(car);
           await _dbContext.SaveChangesAsync();

               return Ok(new { Success = true });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDriver(int driverId)
        {

            var carrierId = _carrierService.Carrier.Id;
            var driver = _dbContext.Drivers.FirstOrDefault(p => p.CarrierId == carrierId && p.Id == driverId);

            if (driver == null) return Json(new { Success = false, Message = "Нет такого водителя" });

            if (_dbContext.RepliesToClaims.Any(r => r.DriverId == driverId))
                return Json(new { Success = false, Message = "Водитель уже используется в документах" });

            _dbContext.Drivers.Remove(driver);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Success = true });
        }
    }
}