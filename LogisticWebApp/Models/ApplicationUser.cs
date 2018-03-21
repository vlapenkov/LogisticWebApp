using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Logistic.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistic.Web.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        [Display(Name = "Перевозчик")]
        [MaxLength(7)]
        [ForeignKey("Carrier")]
        public string CarrierId { set; get; }

        public virtual Carrier Carrier { get; set; }

        [Display(Name = "ФИО")]
        [MaxLength(100)]
        [RegularExpression(@"([А-ЯЁ][а-яё]+[\-\s]?){3,}", ErrorMessage = "Введите ФИО с большой буквы ")]
        public string Fio { get; set; }


        [Required(ErrorMessage = "Введите ИНН")]
        [MaxLength(12)]        
        [StringLength(12)]
        [Display(Name = "ИНН")]
        [RegularExpression("([1-9][0-9]{11}$)", ErrorMessage = "ИНН должно быть 12 цифр")]
        public string Inn { get; set; }

        [Display(Name = "КПП")]
        [MaxLength(9)]
        public string Kpp { get; set; }
        

        [MaxLength(50)]
        public string Pwd { get; set; }

    }
}
