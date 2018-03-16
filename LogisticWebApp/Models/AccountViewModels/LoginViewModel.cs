using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticWebApp.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        
        public string Email { get; set; }

        [Display(Name ="Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}
