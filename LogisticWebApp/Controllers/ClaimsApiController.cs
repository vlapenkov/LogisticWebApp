using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Xml;
using LogisticWebApp.Data;
using Logistic.Data;
using Microsoft.EntityFrameworkCore;
using Logistic.Web.Models;
using Microsoft.Extensions.Logging;

namespace Logistic.Web.Controllers
{
    [Produces("application/xml")]
    [Route("api/Claims")]
    public class ClaimsApiController : Controller

    {

        public class ClaimChosenDto {
            public string carrierId { get; set; }
            public Guid guidOfClaim { get; set; }
        }
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ClaimsApiController> _logger;
        public ClaimsApiController(ApplicationDbContext dbContext, ILogger<ClaimsApiController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        // POST: https://localhost:44325/api/CarrierImport   
        //Content-Length: 13102
        //Content-Type: application/xml
        //<ArrayOfClaimForTransport><ClaimForTransport><CarrierId>94531</CarrierId><Comments>комментарий</Comments><DeadlineDate>2018-11-12T00:00:00</DeadlineDate><DocDate>2018-02-12T00:00:00</DocDate><GuidIn1S>40a0f701-3705-11e7-8505-d4ae52b5e909</GuidIn1S><NumberIn1S>А123456</NumberIn1S><Path>Ярославль - Москва - Васюки</Path><Volume>10</Volume></ClaimForTransport><ClaimForTransport><DocDate>2018-02-02T00:00:00</DocDate><GuidIn1S>cb61ca1f-3705-11e7-8505-d4ae52b5e909</GuidIn1S><NumberIn1S>Ф23443</NumberIn1S><Path>Спб- Витебск - Воронеж</Path><Volume>110</Volume></ClaimForTransport></ArrayOfClaimForTransport>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> Post([FromBody]List<ClaimForTransport> data)
        {

            var carrierIds = _dbContext.Carriers.Select(p => p.Id).ToArray();
            var dataFiltered = data.Where(d => d.CarrierId == null || carrierIds.Contains(d.CarrierId));

            try
            {

                var guidsExisted = _dbContext.ClaimsForTransport.Select(p => p.GuidIn1S).ToArray();
                // заявки которые есть
                var dataExisted = dataFiltered.Where(p => guidsExisted.Contains(p.GuidIn1S)).ToArray();

                if (dataExisted.Length > 0)
                {
                    foreach (var claim in dataExisted)
                    {
                        _dbContext.Attach(claim);
                        _dbContext.Entry(claim).State = EntityState.Modified;
                    }
                    await _dbContext.SaveChangesAsync();
                }

                //Для заявок которых еще нет заполняем дату создания
                var claimsNotExist = dataFiltered.Where(p => !guidsExisted.Contains(p.GuidIn1S)).ToArray();

                Array.ForEach(claimsNotExist, newclaim => newclaim.CreatedDate = DateTime.Now);


                // новые заявки
                if (claimsNotExist.Length > 0)
                {
                    await _dbContext.ClaimsForTransport.AddRangeAsync(claimsNotExist);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }

            return 0;
        }



        // GET: api/Test
        [HttpGet]
        //  [Route("claims")]
        public IQueryable<ClaimForTransport> Get() => _dbContext.ClaimsForTransport.Include(p => p.Replies);

        /// <summary>
        /// Функция подтверждает выбор перевозчика для заявки 
        /// </summary>
        /// <param name="claimdto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("choosecarrier")]
        public IActionResult ChooseCarrier([FromBody]ClaimChosenDto claimdto)
        {
            
            var claim = _dbContext.ClaimsForTransport.FirstOrDefault(p => p.GuidIn1S == claimdto.guidOfClaim);

            if (claim == null)
            {
                _logger.LogError("claim not found" + claimdto.guidOfClaim.ToString());
                throw new NullReferenceException("claim not found" + claimdto.guidOfClaim.ToString());

            }

            try
            {
                claim.CarrierId = claimdto.carrierId;
                claim.Status = StatusOfClaim.Chosen;
                _dbContext.SaveChanges();
                _logger.LogDebug($" Claim carrier chosen: {claim.GuidIn1S} {claim.NumberIn1S}");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
            return Ok(claimdto.guidOfClaim);

        }
    }
}