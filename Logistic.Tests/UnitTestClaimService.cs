using Logistic.Data;
using Logistic.Utils;
using Logistic.Web.Models;
using Logistic.Web.Services;
using LogisticWebApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Logistic.Tests
{
    public class UnitTestClaimService
    {


        private readonly ITestOutputHelper output;

        public UnitTestClaimService(ITestOutputHelper output)
        {
            this.output = output;
        }

        public static ApplicationDbContext GenerateDbContext()
        {

            var uniqueId = Guid.NewGuid().ToString();
            Debug.WriteLine("generating dbcontext:"+uniqueId);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: uniqueId)
              .Options;
           var context = new ApplicationDbContext(options);

            context.ClaimsForTransport.Add(
                    new ClaimForTransport { DocDate = DateTime.Parse("01.01.2018"), GuidIn1S = Guid.NewGuid(), Status = StatusOfClaim.OnTender, Volume = 100 }
                    );
            context.ClaimsForTransport.Add(
                    new ClaimForTransport { DocDate = DateTime.Parse("01.02.2018"), GuidIn1S = Guid.NewGuid(), Status = StatusOfClaim.OnTender, Volume = 30 }
                    );

            context.ClaimsForTransport.Add(
                    new ClaimForTransport { CarrierId = "94639", DocDate = DateTime.Parse("01.03.2018"), GuidIn1S = Guid.NewGuid(), Status = StatusOfClaim.Chosen, Volume = 100 }
                    );
            context.SaveChanges();

            return context;
        }

        /*
        ApplicationDbContext _context;
        public UnitTestClaimService()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
              .Options;
            _context = new ApplicationDbContext(options);

            _context.ClaimsForTransport.Add(
                    new ClaimForTransport {  DocDate = DateTime.Parse("01.01.2018"), GuidIn1S = Guid.NewGuid(), Status = StatusOfClaim.OnTender, Volume = 100 }
                    );
            _context.ClaimsForTransport.Add(
                    new ClaimForTransport {  DocDate = DateTime.Parse("01.02.2018"), GuidIn1S = Guid.NewGuid(), Status = StatusOfClaim.OnTender, Volume = 30 }
                    );

            _context.ClaimsForTransport.Add(
                    new ClaimForTransport { CarrierId = "94639", DocDate = DateTime.Parse("01.03.2018"), GuidIn1S = Guid.NewGuid(), Status = StatusOfClaim.Chosen,Volume = 100 }
                    );
            _context.SaveChanges();

        } */
        [Fact]
        public async Task CountOfClaims_ShouldReturns3()
        {
           var context= GenerateDbContext();
            output.WriteLine($"This is output count is {await context.ClaimsForTransport.CountAsync()}");
            Assert.Equal(3, await context.ClaimsForTransport.CountAsync());                 
        }


        /// <summary>
        /// Проверка по фильтру carrierId
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CountOfClaimsByCarrierId_ShouldReturns3()
        {

            var context = GenerateDbContext();

            var service = new ClaimService(context);

            var model=new ClaimsViewModel { CarrierId = "94639" };

          var claims =await  service.GetFilteredClaimsAsync(model);

            Assert.Equal(3, claims.Count);
        }

        [Fact]
        [InlineData("Foo")]
        public async Task CountOfClaimsByCarrierId_ShouldReturns2()
        {
            var context = GenerateDbContext();
            var service = new ClaimService(context);

            var model = new ClaimsViewModel { CarrierId = "94637" };

            var claims = await service.GetFilteredClaimsAsync(model);

            Assert.Equal(2, claims.Count);
        }

        [Theory]
        [InlineData("94639", 3)]
        [InlineData("91111",2)]
        [InlineData("91114", 5)]
        [InlineData("91113", 2)]
        
        public async Task CountOfClaimsByCarrierId_91111_ShouldReturns(string carrierId,int expectedCount)
        {
            var context = GenerateDbContext();
            var service = new ClaimService(context);

            var model = new ClaimsViewModel { CarrierId = carrierId };

            var claims = await service.GetFilteredClaimsAsync(model);

            Assert.Equal(expectedCount, claims.Count);

            
        }
    }
}
        