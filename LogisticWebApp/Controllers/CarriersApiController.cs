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

namespace Logistic.Web.Controllers
{
    /// <summary>
    /// Данный класс служит для импорта и отображения перевозчиков 
    /// </summary>
    [Produces("application/xml")]
    [Route("api/Carriers")]
    public class CarriersApiController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CarriersApiController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        

        // POST: https://localhost:44325/api/CarrierImport   
        //Content-Length: 13102
        //Content-Type: application/xml
        //<ArrayOfCarrier>
        //<Carrier>
        //<Id>00216  </Id>
        //<FullName>ИП Землянский Александр Николаевич</FullName>
        //<Inn>762703382837</Inn>
        //<Kpp/>
        //</Carrier>
        //<Carrier>
        //	<Id>90215  </Id>
        //	<FullName>ИП Калачев Андрей Борисович</FullName>
        //	<Inn>760402731907</Inn>
        //	<Kpp/>
        //</Carrier>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task< IList<Carrier>> Post([FromBody]List<Carrier> data)
        {

            data.ForEach(record => record.IsActive = true);

            // новые которых нет добавляем
           var existingIds= _dbContext.Carriers.Select(cr => cr.Id);
            var dataFiltered = data.Where(p => !existingIds.Contains(p.Id));

            
            await   _dbContext.Carriers.AddRangeAsync(dataFiltered);

            await _dbContext.SaveChangesAsync();

            // все остальные деактивируем
           var actualIds= data.Select(p => p.Id);

            var carriers=_dbContext.Carriers.Where(p => !actualIds.Contains(p.Id));

            await carriers.ForEachAsync(record => record.IsActive = false);

            await _dbContext.SaveChangesAsync();


           var carriersActual = _dbContext.Carriers.Where(p => actualIds.Contains(p.Id));

            await carriersActual.ForEachAsync(record => record.IsActive = true);

            await _dbContext.SaveChangesAsync();


            return  await _dbContext.Carriers.ToListAsync();
           
        }


        // GET: api/Test
        [HttpGet]
        public IQueryable<Carrier> Get() => _dbContext.Carriers;
        
            
        
    }
}