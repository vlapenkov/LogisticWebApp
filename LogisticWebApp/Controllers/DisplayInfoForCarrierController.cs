using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logistic.Data;
using Logistic.Web.Services;
using LogisticWebApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logistic.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/DisplayInfoForCarrier")]
    public class DisplayInfoForCarrierController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly CarrierService _carrierService;
        private readonly ClaimService _claimsService;


        public DisplayInfoForCarrierController(ApplicationDbContext dbContext, CarrierService carrierService, ClaimService claimsService)
        {
            _dbContext = dbContext;
            _carrierService = carrierService;
            _claimsService = claimsService;
        }

        
        [HttpGet]
        [Route("GetActiveClaims")]
        // http://localhost:61564/api/DisplayInfoForCarrier/getactiveclaims
        public async Task<int> GetActiveClaims()
        {
           var carrierId= _carrierService.Carrier.Id;
            return await _claimsService.GetNumberOfActiveClaimsWithNoReplies(carrierId);
        }

        
        [HttpGet]
        // http://localhost:61564/api/DisplayInfoForCarrier/getcarcount
        public async Task<int> GetCarsCount()
        {
            var carrierId = _carrierService.Carrier.Id;
            return ( await _carrierService.GetCars(carrierId)).Count();
        }


        [HttpGet]
        [Route("GetLastActiveClaim")]
        // http://localhost:61564/api/DisplayInfoForCarrier/getlastactiveclaim
        public async Task<Guid> GetLastActiveClaim()
        {         
            return (await _claimsService.GetLastActiveClaim());
        }

    }
}
