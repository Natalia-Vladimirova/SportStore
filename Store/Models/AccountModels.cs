using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A password length must contain at least {2} symbols.", MinimumLength = 6)]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Password should contain only letters and digits.")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password confirmation")]
        [Compare("NewPassword", ErrorMessage = "New password and confirmed password are not equal.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "User name should contain only letters and digits.")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_.-])+@([a-zA-Z0-9_.-])+\.[a-zA-Z]+$", ErrorMessage = "Entered email is incorrect.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A password length must contain at least {2} symbols.", MinimumLength = 6)]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Password should contain only letters and digits.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password confirmation")]
        [Compare("Password", ErrorMessage = "New password and confirmed password are not equal.")]
        public string ConfirmPassword { get; set; }
    }
}
