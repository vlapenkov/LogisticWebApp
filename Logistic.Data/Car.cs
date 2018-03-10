using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistic.Data
{
    /// <summary>
    /// Информация об автомобиле 
    /// </summary>
    public class Car
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [Required]
        [MaxLength(50)]
        public string StateNumber { get; set; }

        [Required]
        public int Volume { get; set; }


        public bool IsActive { get; set; }

        /// <summary>
        /// Привязка в водителю (может быть не выбран)
        /// </summary>
         [ForeignKey("Driver")]
        public int? DriverId { get; set; }

        public virtual Driver Driver { get; set; }

        [MaxLength(7)]
        [ForeignKey("Carrier")]
        public string CarrierId { set; get; }

        public virtual Carrier Carrier { get; set; }


        /// <summary>
        ///  Путь к фото
        /// </summary>
        [MaxLength(Byte.MaxValue)]
        public string FilePath { get; set; }

    }
}
