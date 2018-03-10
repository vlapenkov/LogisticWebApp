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

namespace Logistic.Web.Controllers
{
    [Produces("application/xml")]
    [Route("api/Claims")]
    public class ClaimsApiController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public ClaimsApiController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // POST: https://localhost:44325/api/CarrierImport   
        //Content-Length: 13102
        //Content-Type: application/xml
        //<ArrayOfClaimForTransport><ClaimForTransport><CarrierId>94531</CarrierId><Comments>комментарий</Comments><DeadlineDate>2018-11-12T00:00:00</DeadlineDate><DocDate>2018-02-12T00:00:00</DocDate><GuidIn1S>40a0f701-3705-11e7-8505-d4ae52b5e909</GuidIn1S><NumberIn1S>А123456</NumberIn1S><Path>Ярославль - Москва - Васюки</Path><Volume>10</Volume></ClaimForTransport><ClaimForTransport><DocDate>2018-02-02T00:00:00</DocDate><GuidIn1S>cb61ca1f-3705-11e7-8505-d4ae52b5e909</GuidIn1S><NumberIn1S>Ф23443</NumberIn1S><Path>Спб- Витебск - Воронеж</Path><Volume>110</Volume></ClaimForTransport></ArrayOfClaimForTransport>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> Post([FromBody]List<ClaimForTransport> data)
        {
 
            // новые заявки
            var claimsNotExist = data.Where(p =>  !_dbContext.ClaimsForTransport.Select(cr => cr.GuidIn1S).Contains(p.GuidIn1S)).ToArray();


            if (claimsNotExist.Length > 0)
            {
                await _dbContext.ClaimsForTransport.AddRangeAsync(claimsNotExist);
                await _dbContext.SaveChangesAsync();
            }

            
            // заявки которые есть
            var dataExisted = data.Where(p => _dbContext.ClaimsForTransport.Select(cr => cr.GuidIn1S).Contains(p.GuidIn1S)).ToArray();

            if (dataExisted.Length > 0)
            {
                foreach (var claim in dataExisted)
                {
                    _dbContext.Attach(claim);
                    _dbContext.Entry(claim).State = EntityState.Modified;
                }
                await _dbContext.SaveChangesAsync();
            }

            return 0;
        } 

            /*
            [HttpPost]
        public IList<ClaimForTransportDto> Post([FromBody]string content)
        {

            return new[]
          { new ClaimForTransportDto { DocDate= DateTime.Now,GuidIn1S=Guid.NewGuid(),NumberIn1S="123",Path="",Volume=10},
            new ClaimForTransportDto { DocDate = DateTime.Now, GuidIn1S = Guid.NewGuid(), NumberIn1S = "123", Path = "",ReadyDate=DateTime.Now }
          };
        }
        */

        // GET: api/Test
        [HttpGet]
      //  [Route("claims")]
        public IQueryable<ClaimForTransport> Get() => _dbContext.ClaimsForTransport;
/*
        [HttpGet]
        [Route("files")]
        public IQueryable<FileModel> GetFiles() => _dbContext.Files;


        [HttpGet]
        [Route("claimsdto")]
        public IList<ClaimForTransportDto> GetClaims()
        {
            return new []
          { new ClaimForTransportDto { DocDate= DateTime.Now,GuidIn1S=Guid.NewGuid(),NumberIn1S="123",Path="",Volume=10},
            new ClaimForTransportDto { DocDate = DateTime.Now, GuidIn1S = Guid.NewGuid(), NumberIn1S = "123", Path = "",ReadyDate=DateTime.Now }
          };


        }
        */
    }
}