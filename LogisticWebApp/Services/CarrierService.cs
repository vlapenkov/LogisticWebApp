using Logistic.Data;
using Logistic.Web.Models;
using LogisticWebApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistic.Web.Services
{
    public class CarrierService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public CarrierService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _dbContext = dbContext;
        }

       
        /// <summary>
        /// Получить связанного с текущим пользователем перевозчика
        /// </summary>
        public Carrier Carrier { get { 
            
                var userClaim = _httpContextAccessor.HttpContext.User;
                if (userClaim == null) return null;
           var user=  _dbContext.Users.Include(p => p.Carrier).FirstOrDefault(p => p.UserName == userClaim.Identity.Name);
        
                return user?.Carrier;
            }
            
        }


        public async Task<IList<Car>> GetCars(string carrierId) =>  await _dbContext.Cars.Where(p => p.CarrierId == carrierId).ToListAsync();

        public async Task<IList<Driver>> GetDrivers(string carrierId) => await  _dbContext.Drivers.Where(p => p.CarrierId == carrierId).ToListAsync();


        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _dbContext.Users.Include(p => p.Carrier).FirstAsync(user => user.Id == userId);
        }

    }
}
