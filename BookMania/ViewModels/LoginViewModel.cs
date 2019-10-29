using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.ViewModels
{
    public class LoginViewModel
    {
        [DataType(DataType.Text)]
        [Required]
        [Display(Name = "Email/UserName")]
        public string EmailUserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool PersistUser { get; set; }
    }
}
