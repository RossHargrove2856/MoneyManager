using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MoneyManager.Models;
using MoneyManager.Controllers;

namespace MoneyManager.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserContext _context;
        private readonly UserManager<User> _userManager;

        public AccountsController(UserContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task<User> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        [HttpGet]
        [Route("/Accounts")]
        public IActionResult Index()
        {
            var currentUser = GetCurrentUser();
            if(currentUser == null)
            {
                return RedirectToAction("Index", "Users");
            }
            ViewBag.User = _context.users.Include(user => user.Accounts).Where(result => result.Id == currentUser.Result.Id).First();
            var userAccounts = ViewBag.User;
            return View("Dashboard");
        }

        [HttpGet]
        [Route("/Accounts/Add")]
        public IActionResult AddAccount()
        {
            return View("AddAccount");
        }

        [HttpPost]
        [Route("/Accounts/Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAccount(AccountViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = GetCurrentUser();
                Account account = new Account 
                {
                    AccountName = model.AccountName,
                    Balance = model.Balance,
                    UserId = user.Result.Id
                };
                _context.accounts.Add(account);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("AddAccount", model);
        }

        [HttpGet]
        [Route("/Account/{accountId}")]
        public IActionResult GetAccountById(int accountId)
        {
            ViewBag.Account = _context.accounts.Include(match => match.Transactions).Where(result => result.AccountId == accountId).First();
            return View("AccountInfo");
        }

        [HttpPost]
        [Route("/Account/Transaction")]
        [ValidateAntiForgeryToken]
        public IActionResult Transaction(TransactionViewModel model)
        {
            if(ModelState.IsValid)
            {
                var userAccount = _context.accounts.Include(account => account.Transactions).SingleOrDefault(result => result.AccountId == model.AccountId);
                if(model.TransactionAmount < 0 && Math.Abs(model.TransactionAmount) >= userAccount.Balance)
                {
                    ModelState.AddModelError(string.Empty, "The withdrawal amount cannot be more than the account balance.");
                    return RedirectToAction("GetAccountById", new {accountId = model.AccountId});
                }
                userAccount.Balance += model.TransactionAmount;
                Transaction transaction = new Transaction
                {
                    TransactionAmount = model.TransactionAmount,
                    TransDate = DateTime.Now,
                    AccountId = model.AccountId
                };
                _context.transactions.Add(transaction);
                _context.SaveChanges();
                return RedirectToAction("GetAccountById", new {accountId = model.AccountId});
            }
            return RedirectToAction("GetAccountById", new {accountId = model.AccountId});
        }

        [HttpPost]
        [Route("/Account/Delete")]
        public IActionResult DeleteAccount(int AccountId)
        {
            var userAccount = _context.accounts.Include(match => match.Transactions).SingleOrDefault(match => match.AccountId == AccountId);
            _context.accounts.Remove(userAccount);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}