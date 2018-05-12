using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MoneyManager.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime TransDate { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}