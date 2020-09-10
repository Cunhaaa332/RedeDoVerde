using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;
using RedeDoVerde.Services.Account;
using RedeDoVerde.Web.ViewModel.Account;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;

namespace RedeDoVerde.Web.Controllers
{
    public class AccountController : Controller
    {

        private IAccountService _accountService { get; set; }
        private IAccountIdentityManager _accountIdentityManager { get; set; }

        public AccountController(IAccountService accountService, IAccountIdentityManager accountIdentityManager)
        {
            _accountService = accountService;
            _accountIdentityManager = accountIdentityManager;
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
                var result = await _accountIdentityManager.Login(model.Email, model.Password);
                if(!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Login ou Senha inválidos");
                    return View(model);
                }

                var client = new RestClient();
                var request = new RestRequest("https://localhost:44386/api/authenticate/token", DataFormat.Json);
                request.AddJsonBody(model);
                var response = client.Post<string>(request);
                HttpContext.Session.SetString("Token", response.Data);

                if (!String.IsNullOrWhiteSpace(returnUrl))
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
        public IActionResult Register()
        {
            
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    await _accountService.Register(model.Name, model.DtBirthday, model.Email, model.Password);
                    return Redirect("/");
                }
                return View(model);
            }
            catch 
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro, por favor tente mais tarde.");
                return View(model);
            }
        }

        public IActionResult Logout()
        {
            _accountIdentityManager.Logout();
                foreach (var cookie in HttpContext.Request.Cookies)
                {
                    Response.Cookies.Delete(cookie.Key);
                }
            return Redirect("/Account/Login");
        }
    }
}