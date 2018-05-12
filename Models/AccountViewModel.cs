using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MoneyManager.Models
{
    public class AccountViewModel : BaseEntity
    {
        [Required]
        public string AccountName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }
    }
}