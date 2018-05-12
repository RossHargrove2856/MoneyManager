using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MoneyManager.Models
{
    public class TransactionViewModel : BaseEntity
    {
        [Required]
        [DataType(DataType.Currency)]
        public decimal TransactionAmount { get; set; }

        [Required]
        public int AccountId { get; set; }
    }
}