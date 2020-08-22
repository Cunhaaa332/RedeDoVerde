using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedeDoVerde.Services.Account;

namespace RedeDoVerde.Web.Controllers
{
    public class AccountController : Controller
    {

        private IAccountService AccountService { get; set; }

        public AccountController(IAccountService accountService)
        {
            AccountService = accountService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}