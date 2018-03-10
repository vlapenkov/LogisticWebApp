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
    /// Model for response to claims
    /// </summary>
    /// 

    public class ResponseViewModel
    {

        public Guid GuidOfClaimIn1S { get; set; }

        [Required(ErrorMessage ="Введите дату подачи авто")]
        [Display(Name = "Дата подачи авто")]
        public DateTime? ArrivalDate { get; set; }

        [Required(ErrorMessage = "Введите дату разгрузки")]
        [Display(Name = "Дата разгрузки")]
        public DateTime? UnloadDate { get; set; }

        [Required]
        [RegularExpression("\\d+", ErrorMessage = "Введите число")]
        [Display(Name = "Стоимость")]
        [Range(1, 100000, ErrorMessage = "Введите значение {0} от {1} до {2}")]
        public decimal  Cost { get; set; }

        [Required(ErrorMessage = "Укажите автомобиль")]
        public int? CarId { set; get; }


        [Required(ErrorMessage = "Укажите водителя")]
        public int? DriverId { set; get; }


        public NdsState NdsState { get; set; }

    }

    
}
