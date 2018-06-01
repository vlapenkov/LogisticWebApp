using Logistic.Data;
using Logistic.Web.Models;
using LogisticWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Logistic.Web.Services
{
    /// <summary>
    /// Сервис отбора по заявкам
    /// </summary>
    public class ClaimService
    {
        private readonly ApplicationDbContext _context;

        public ClaimService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отбор заявок 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task< IList<ClaimWithOneReplyVm>>  GetFilteredClaimsAsync(ClaimsViewModel model)
        {
                      
            IQueryable<ClaimForTransport> claims = _context.ClaimsForTransport.Include(reply => reply.Replies).ThenInclude(p => p.Driver).
                Include(reply => reply.Replies).ThenInclude(p => p.Car).
                Where(p => model.SearchString == null || p.NumberIn1S.Contains(model.SearchString)).
                Where(p => model.StartDate == null || p.DocDate >= model.StartDate).
                Where(p => model.EndDate == null || p.DocDate <= model.EndDate);

            if (model.Volume != null) claims = claims.Where(p => p.Volume <= (int)model.Volume.Value);

            claims = claims.Where(p => p.Status == StatusOfClaim.OnTender || p.CarrierId == model.CarrierId).OrderByDescending(p=>p.DocDate);

           


            var claimsResult = (await claims.AsNoTracking().ToListAsync()).Select(p => new ClaimWithOneReplyVm
            {
                GuidIn1S = p.GuidIn1S,
                DocDate = p.DocDate,
                NumberIn1S = p.NumberIn1S,
                Path = p.Path,
                ReadyDate = p.ReadyDate,
                DeadlineDate = p.DeadlineDate,
                Volume = p.Volume,
                Comments = p.Comments,
                Status = p.Status,
                CarrierId = p.CarrierId,
                Reply = p.Replies.FirstOrDefault(x => x.CarrierId == model.CarrierId),
                OtherRepliesExists=p.Replies.Any(reply=> reply.CarrierId!=model.CarrierId)
            });

            switch (model.FilterByStatus)
            {

                case FilterByStatus.OnlyNew:
                    {
                        claimsResult = claimsResult.Where(p => p.Status == StatusOfClaim.OnTender && p.Reply == null);
                        break;
                    }
                case FilterByStatus.OnlyApproved:
                    {
                        claimsResult = claimsResult.Where(p => p.Status >= StatusOfClaim.Chosen && p.CarrierId == model.CarrierId);
                        break;
                    }
                default:
                    break;
            }

            return   claimsResult.ToList();
        }

        /// <summary>
        /// Получить число заявок без ответов
        /// </summary>
        /// <param name="carrierId"></param>
        /// <returns></returns>
        public async Task<int> GetNumberOfActiveClaimsWithNoReplies(string carrierId)
        {

            IQueryable<ClaimForTransport> claims = _context.ClaimsForTransport.Include(reply => reply.Replies);
                
                
            claims = claims.Where(p => p.Status == StatusOfClaim.OnTender).OrderByDescending(p => p.DocDate);

            
            return (await claims.AsNoTracking().ToListAsync()).Select(p => new ClaimWithOneReplyVm
            {
                GuidIn1S = p.GuidIn1S,            
                CarrierId = p.CarrierId,
                Reply = p.Replies.FirstOrDefault(x => x.CarrierId == carrierId)
            }).Where(p => p.Reply == null).Count();
                        
        }

        public async Task<Guid> GetLastActiveClaim()
        {
           return await _context.ClaimsForTransport.Where(p => p.Status == StatusOfClaim.OnTender).OrderByDescending(p => p.DocDate).Select(claim => claim.GuidIn1S).FirstOrDefaultAsync();
            
        }
    }
}
