using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Logistic.Data
{
    
    
    
    /// <summary>
    /// Заявка на транспорт
    /// </summary>
    /// 
    [System.Xml.Serialization.XmlRoot("ClaimForTransport", IsNullable = true)]
    [DataContract(Namespace = "")]
    public class ClaimForTransport
    {
    
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataMember]
        public Guid GuidIn1S { get; set; }

        /// <summary>
        /// Дата и время заявки
        /// </summary>
        [Required]
        [DataMember]
        public DateTime DocDate { get; set; }
                
        [Required]
        [MaxLength(10)]
        [DataMember]
        public string NumberIn1S { get; set; }


        /// <summary>
        /// Маршрут
        /// </summary>
        [Required]
        [DataMember]
        public string Path { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public DateTime? ReadyDate { get; set; }

        [DataMember(EmitDefaultValue =false)]
        public DateTime? DeadlineDate { get; set; }

       [DataMember]
        public int  Volume { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Comments { get; set; }

        [DataMember]
        public StatusOfClaim Status { get; set; }

       
        public virtual List<ReplyToClaim> Replies { get; set; }

        [MaxLength(7)]
        [ForeignKey("Carrier")]
        [DataMember(EmitDefaultValue = false)]
        public string CarrierId { set; get; }

        
        public virtual Carrier Carrier { get; set; }

    }

    /// <summary>
    /// Статус заявки
    /// </summary>
    [DataContract(Name = "Status")]
    public enum StatusOfClaim
    {
        [Display(Name = "в поиске")]
        
        // нужен для десеарилизации заявок (статус передается как число)
        [EnumMember(Value ="0")]
        InSearch =0,

        [Display(Name = "на тендере")] //1
        [EnumMember(Value = "1")]
        OnTender =1 ,

        [Display(Name = "есть ответ")] //2
        [EnumMember(Value = "2")]
        Responded =2 ,

        [Display(Name = "перевозчик выбран")] //3
        [EnumMember(Value = "3")]
        Chosen =3,

        [Display(Name = "на погрузке")] //4
        [EnumMember(Value = "4")]
        OnLoad ,

        [Display(Name = "в пути")] //5
        [EnumMember(Value = "5")]
        OnWay ,

        [Display(Name = "выполнена")] //6
        [EnumMember(Value = "6")]
        Done ,

        [Display(Name = "закрыта")] //7
        [EnumMember(Value = "7")]
        Closed
    }
}
