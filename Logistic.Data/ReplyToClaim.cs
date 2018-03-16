using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Logistic.Data
{
    /// <summary>
    /// Ответы на заявки
    /// </summary>
      [DataContract(Namespace = "")]
    public class ReplyToClaim
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Claim")]
        public Guid GuidOfClaimIn1S { get; set; }
        public virtual ClaimForTransport Claim { get; set; }

        [Key, Column(Order = 2)]
        [MaxLength(7)]
        [ForeignKey("Carrier")]
        [DataMember]
        public string CarrierId { set; get; }

      //  [DataMember]
        public virtual Carrier Carrier { get; set; }

        /// <summary>
        /// Дата подачи авто
        /// </summary>
        [DataMember]
        public DateTime? ArrivalDate { get; set; }

        /// <summary>
        /// Дата подачи авто
        /// </summary>
        [DataMember]
        public DateTime? UnloadDate { get; set; }

        [DataMember]
        public decimal Cost { get; set; }

        [DataMember]
        [ForeignKey("Car")]
        public int? CarId { get; set; }

        public virtual Car Car { get; set; }

        [DataMember]
        [ForeignKey("Driver")]
        public int? DriverId { get; set; }

        public virtual Driver Driver { get; set; }

    }
}
