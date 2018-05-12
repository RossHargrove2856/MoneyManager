using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MoneyManager.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Required(ErrorMessage = "First name cannot be blank")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name cannot be blank")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username cannot be blank")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email cannot be blank")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Password confirmation cannot be blank")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}