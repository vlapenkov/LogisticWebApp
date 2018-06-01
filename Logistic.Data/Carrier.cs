using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Logistic.Data
{
    /// <summary>
    /// Данные о перевозчике
    /// </summary>
    
        [DataContract(Namespace ="")]
   public class Carrier
    {
        /// <summary>
        /// Код перевозчика в 1С
        /// </summary>
        
           [DataMember]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(7)]
        public string Id { get; set; }

        [DataMember]
        [Required]
        [MaxLength(12)]
        public string Inn { get; set; }
        [DataMember]
        [Required]
        [MaxLength(9)]
        public string Kpp { get; set; }

        /// <summary>
        /// Полное наименование организации-перевозчика
        /// </summary>
         [DataMember]
        [Required]
        [MaxLength(Byte.MaxValue)]
        public string FullName { get; set; }

        // активный перевозчик (виден при выборе)
        public bool IsActive { get; set; }


    }
}
