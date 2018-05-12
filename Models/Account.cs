using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MoneyManager.Models
{
    public class Account : BaseEntity
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public Account()
        {
            Transactions = new List<Transaction>();
        }
    }
}