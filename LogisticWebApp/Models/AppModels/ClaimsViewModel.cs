using Logistic.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Logistic.Web.Models
{
    /// <summary>
    /// Диапазоны объема (включая )
    /// </summary>
    public enum VolumeRanges {
       /* [Display(Name = "Объем ")]
        All = 0, */
        [Display(Name = "до 16")]
        Till16=16,
        [Display(Name = "до 45")]
        Till45=45,
        [Display(Name = "до 86")]
        Till86=86,
        
    }

   public  enum FilterByStatus {
        All=0,
        OnlyNew=1,
        OnlyApproved=2
    }
    /// <summary>
    /// Model for claims
    /// </summary>
    public class ClaimsViewModel
    {
        //public FilterByType FilterByType { get; set; }

        public FilterByStatus FilterByStatus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // public IPagedList<ClaimForTransport> Claims { get; set; }
        public IPagedList<ClaimWithOneReplyVm> Claims { get; set; }

        public VolumeRanges? Volume { get; set; }

        public Guid GuidOfClaimIn1S { get; set; }

        public string CarrierId { set; get; }


        [Required]
        public DateTime? ArrivalDate { get; set; }

        /// <summary>
        /// Дата подачи авто
        /// </summary>
        [Required]
        public DateTime? UnloadDate { get; set; }

        [Required(ErrorMessage = "Введите стоимость")]
        public decimal Cost { get; set; }

        public NdsState NdsState { get; set; }


        public int? CarId { get; set; }

        public int? DriverId {get; set;}

        public IList<Driver> Drivers { get; set; }

        public IList<Car> Cars { get; set; }
        public string SearchString { get; internal set; }
    }

    public enum NdsState{
        WithNds,
        NoNds
    }
}
