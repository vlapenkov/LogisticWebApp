using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistic.Data
{
    /// <summary>
    /// Информация о Водителе
    /// </summary>
    public class Driver
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Fio { get; set; }

        [RegularExpression(@"\d{11}$",ErrorMessage ="номер телефона - 11 цифр без разделителей")]
        [Required]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }


        /// <summary>
        /// Есть договор
        /// </summary>
        public bool HasContract { get; set; }

        public bool IsActive { get; set; }


        [MaxLength(7)]
        [ForeignKey("Carrier")]
        public string CarrierId { set; get; }

        public virtual Carrier Carrier { get; set; }


        [MaxLength(Byte.MaxValue)]
        public string FilePath { get; set; }

        public override string ToString()
        {

            return $"Driver fio is {this.Fio}, and PhoneNumber  is {this.PhoneNumber} with hashCode {this.GetHashCode()}";
        }

    }
}
