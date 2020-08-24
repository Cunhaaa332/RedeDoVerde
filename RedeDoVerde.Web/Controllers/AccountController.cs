using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedeDoVerde.Services.Account;
using RedeDoVerde.Web.ViewModel.Account;

namespace RedeDoVerde.Web.Controllers
{
    public class AccountController : Controller
    {

        private IAccountService AccountService { get; set; }
        private IAccountIdentityManager AccountIdentityManager { get; set; }

        public AccountController(IAccountService accountService, IAccountIdentityManager accountIdentityManager)
        {
            AccountService = accountService;
            AccountIdentityManager = accountIdentityManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                var result = await AccountIdentityManager.Login(model.Email, model.Password);
                if(!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Login ou Senha inválidos");
                    return View(model);
                }

                if(!String.IsNullOrWhiteSpace(returnUrl))
                {
                    return Redirect(returnUrl);
                } 
                return Redirect("/");
            }
            catch 
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro, por favor tente mais tarde.");
                return View(model);
            }
        }
    }
}