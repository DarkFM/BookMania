using System.ComponentModel.DataAnnotations;

namespace BookMania.ViewModels
{
    public class LoginVM
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
