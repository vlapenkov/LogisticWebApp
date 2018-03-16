using Logistic.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Logistic.Web.Models
{
    public class ApplicationUserVm
    {

        
        public ApplicationUserVm(string id, string username, string fio, string inn, string kpp, string carrierId)
        {
            Id = id;
            Username = username;
            Fio = fio;
            Inn = inn;
            Kpp = kpp;
            CarrierId = carrierId;
        }

        public ApplicationUserVm()
        {
        }


        public IList<Carrier> Carriers { get; set; }
        
        public virtual Carrier Carrier { get; set; }
               
        public string Id { get; set; }

        public string Username { get; private set; }


        [Display(Name = "ФИО")]
        
        [RegularExpression(@"([А-ЯЁ][а-яё]+[\-\s]?){3,}", ErrorMessage = "Введите ФИО с большой буквы ")]
        public string Fio { get; set; }

        [Required(ErrorMessage = "Введите ИНН")]
        [StringLength(12)]
        [Display(Name = "ИНН")]
        [RegularExpression("([1-9][0-9]{11}$)", ErrorMessage = "ИНН должно быть 12 цифр")]
        public string Inn { get; set; }

        [Display(Name = "КПП")]
        public string Kpp { get; set; }

        [Display(Name = "Перевозчик")]
        public string CarrierId { set; get; }


        public IList<Car> Cars { get; set; }
        public IList<Driver> Drivers { get; set; }
    }
}
