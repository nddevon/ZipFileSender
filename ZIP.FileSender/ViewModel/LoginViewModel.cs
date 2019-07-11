using System.ComponentModel.DataAnnotations;

namespace ZIP.FileSender.ViewModel {
    public class LoginViewModel {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "User name is required")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
