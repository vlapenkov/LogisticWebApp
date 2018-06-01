using Logistic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistic.Web.Models
{
    /// <summary>
    /// Модель для заявок для перевозчика
    /// </summary>
    public class ClaimWithOneReplyVm
    {

        public Guid GuidIn1S { get; set; }
        public DateTime DocDate { get; set; }
        public string NumberIn1S { get; set; }
        public string Path { get; set; }
        public DateTime? ReadyDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public int Volume { get; set; }
        public string Comments { get; set; }
        public StatusOfClaim Status { get; set; }
        public string CarrierId { set; get; }
        public ReplyToClaim Reply { get; set; }
        public bool OtherRepliesExists { get; set; }
    }
}
