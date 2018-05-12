using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MoneyManager.Models
{
    public class LoginViewModel : BaseEntity
    {
        [Required(ErrorMessage = "Username cannot be blank")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password cannot be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}