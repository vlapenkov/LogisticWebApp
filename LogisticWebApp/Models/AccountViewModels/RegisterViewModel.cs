using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticWebApp.Models.AccountViewModels
{
    public class RegisterViewModel
    {               
        
        [Required (ErrorMessage = "Введите Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(100, ErrorMessage = "Пароль {0} должен быть как минимум {2} и максимум {1}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение должны совпадать.")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Введите ИНН")]
        [StringLength(12)]
        [Display(Name = "ИНН")]
        [RegularExpression("([1-9][0-9]{8}[0-9]{0,3}$)", ErrorMessage ="ИНН должно быть 9-12 цифр")]
        public string Inn { get; set; }

        /*   [Required] */
        //[Required(ErrorMessage = "Введите Кпп")]
        [Display(Name = "Кпп")]
        [StringLength(9)]
        public string Kpp { get; set; }

        [Required(ErrorMessage = "Введите ФИО")]
        [Display(Name = "ФИО")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+[\-\s]?){3,}", ErrorMessage ="Введите ФИО с большой буквы ")]
        [StringLength(100)]
        public string Fio { get; set; }
    }
}
