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
using Logistic.Web.Services;
using LogisticWebApp.Services;

namespace Logistic.Web.Controllers
{
    /// <summary>
    /// Класс для загрузки и отображения заявок через API
    /// </summary>
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
        private readonly FcmService  _fcmservice;
        private readonly IEmailSender _emailSender;




        public ClaimsApiController(ApplicationDbContext dbContext, ILogger<ClaimsApiController> logger, FcmService fcmservice, IEmailSender emailSender)
        {
            _dbContext = dbContext;
            _logger = logger;
            _fcmservice = fcmservice;
            _emailSender = emailSender;
        }


        // POST: https://localhost:44325/api/claims
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
                        //_dbContext.Attach(claim);
                        //  _dbContext.Entry(claim).State = EntityState.Modified;

                       var claimFound= await _dbContext.ClaimsForTransport.FirstOrDefaultAsync(p => p.GuidIn1S == claim.GuidIn1S);
                       
                        claimFound.Status = claim.Status;
                        claimFound.Comments = claim.Comments;

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

                    string[] emails = await _dbContext.Users.Where(p => p.CarrierId != null).Select(u => u.Email).ToArrayAsync();
                    foreach (var email in emails)
                        await _emailSender.SendEmailAsync(email, "Новые заявки загружены в систему логистики", String.Format("новые заявки загружены  {0}, перейдите по ссылке <a href=\"https://logistic.yst.ru\">в систему</a>", DateTime.Now));

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }

            return dataFiltered.Count();
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
        public async Task<IActionResult> ChooseCarrier([FromBody]ClaimChosenDto claimdto)
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

                // отправляем сообщение по почте
                var emailChosen = _dbContext.Users.FirstOrDefault(p => p.CarrierId == claimdto.carrierId)?.Email;
                if (!String.IsNullOrEmpty(emailChosen)) await _emailSender.SendEmailAsync(emailChosen, $"По заявке {claim.NumberIn1S} вы выбраны перевозчиком", String.Format("вы выбраны перевозчиком  {0}, перейдите по ссылке <a href=\"https://logistic.yst.ru\">в систему</a>", DateTime.Now));

               var carrierIds= claim.Replies.Where(p => p.CarrierId != claimdto.carrierId).Select(c => c.CarrierId);

                if (carrierIds.Any())
                {
                   var emails= await _dbContext.Users.Where(p => carrierIds.Contains( p.CarrierId )).Select(u => u.Email).ToArrayAsync();
                    foreach (var email in emails)
                        await _emailSender.SendEmailAsync(emailChosen, $"По заявке {claim.NumberIn1S} выбран другой перевозчик", String.Format("По заявке выбран другой перевозчик <a href=\"https://logistic.yst.ru\">в систему</a>", DateTime.Now));

                }


            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }


            

            // отправка уведомления через fcm
            try
            {
                var userForClaim = _dbContext.Users.FirstOrDefault(p => p.CarrierId == claimdto.carrierId);

                if (userForClaim != null)
                {
                    var userId = userForClaim.Id;

                    var claimValue = _dbContext.UserClaims.FirstOrDefault(p => p.UserId == userId && p.ClaimType == "token")?.ClaimValue;

                    if (claimValue != null) _fcmservice.SendNotification($"По заявке {claim.NumberIn1S} вы выбраны перевозчиком", claimValue);
                    _logger.LogDebug($" fcm message sent to : {claimValue}");
                }
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