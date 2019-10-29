using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.ViewModels
{
    public class RegisterViewModel
    {
        [DataType(DataType.Text)]
        [Required]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Profile Name")]
        [Required]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Remote(action: "VerifyEmail", controller: "Account")]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
