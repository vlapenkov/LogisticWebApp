using Logistic.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Logistic.Web.Models
{
    public class CarsAndDriversViewModel
    {
        public Carrier Carrier { get; set; }
        public IList<Car> Cars { get; set; }
        public IList<Driver> Drivers { get; set; }


        // }


        // public class CarVm {
        /// <summary>
        /// Car info
        /// </summary>
        /// 
        [Required]
        [MaxLength(50)]
        [Display(Name = "Марка")]
        public string Brand { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Модель")]
        public string CarModel { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Гос. номер")]
        public string StateNumber { get; set; }


        [Display(Name = "Объем")]
        [Required]
        public int Volume { get; set; }
        // }

        // public class DriverVm {
        /// <summary>
        /// Driver info
        /// </summary>
        [Display(Name = "ФИО")]
        [Required]
        [StringLength(100)]
        [RegularExpression(@"([А-ЯЁ][а-яё]+[\-\s]?){3,}", ErrorMessage = "Введите ФИО с большой буквы ")]
        public string Fio { get; set; }

        [Display(Name = "Тел. номер")]
        [RegularExpression(@"\d{11}$", ErrorMessage = "номер телефона - 11 цифр без разделителей")]
        [Required]
        [StringLength(11)]
        public string PhoneNumber { get; set; }


        /// <summary>
        /// Есть договор
        /// </summary>
        public bool HasContract { get; set; }
    }


    public class DriverVm {
    /// <summary>
    /// Driver info
    /// </summary>
    [Display(Name = "ФИО")]
    [Required]
    [StringLength(100)]
    [RegularExpression(@"([А-ЯЁ][а-яё]+[\-\s]?){3,}", ErrorMessage = "Введите ФИО с большой буквы ")]
    public string Fio { get; set; }

    [Display(Name = "Тел. номер")]
    [RegularExpression(@"\d{11}$", ErrorMessage = "номер телефона - 11 цифр без разделителей")]
    [Required]
    [StringLength(11)]
    public string PhoneNumber { get; set; }


    /// <summary>
    /// Есть договор
    /// </summary>
    public bool HasContract { get; set; }
}

     public class CarVm {
    /// <summary>
    /// Car info
    /// </summary>
    /// 
    [Required]
    [MaxLength(50)]
    [Display(Name = "Марка")]
    public string Brand { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Модель")]
    public string CarModel { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Гос. номер")]
    public string StateNumber { get; set; }


    [Display(Name = "Объем")]
    [Required]
    public int Volume { get; set; }
     }
}
