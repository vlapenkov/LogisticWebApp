using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Logistic.Data
{
    /// <summary>
    /// Ответы на заявки
    /// </summary>
   public class ReplyToClaim
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Claim")]
        public Guid GuidOfClaimIn1S { get; set; }
        public virtual ClaimForTransport Claim { get; set; }

        [Key, Column(Order = 2)]
        [MaxLength(7)]
        [ForeignKey("Carrier")]
        public string CarrierId { set; get; }
        public virtual Carrier Carrier { get; set; }

        /// <summary>
        /// Дата подачи авто
        /// </summary>
        public DateTime? ArrivalDate { get; set; }

        /// <summary>
        /// Дата подачи авто
        /// </summary>
        public DateTime? UnloadDate { get; set; }

        public decimal Cost { get; set; }


        [ForeignKey("Car")]
        public int? CarId { get; set; }

        public virtual Car Car { get; set; }

        [ForeignKey("Driver")]
        public int? DriverId { get; set; }

        public virtual Driver Driver { get; set; }

    }
}
