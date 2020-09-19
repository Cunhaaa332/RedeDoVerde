﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RedeDoVerde.Domain.Account.Repository;
using RedeDoVerde.Domain.Comment;
using RedeDoVerde.Domain.Post;
using RedeDoVerde.Repository.Account;
using RestSharp;

namespace RedeDoVerde.Web.Controllers
{
    public class PostController : Controller
    {

        private readonly IAccountRepository _accountRepository;

        public PostController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        // GET: PostsController
        public ActionResult Index()
        {
            var client = new RestClient();
            var key = HttpContext.Session.GetString("Token");

            var request = new RestRequest("https://localhost:44386/api/posts", DataFormat.Json);
            var response = client.Get<List<Post>>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

            return View(response.Data);
        }

        // GET: PostsController/Details/5
        public ActionResult Details(Guid id)
        {
            var client = new RestClient();
            var key = HttpContext.Session.GetString("Token");

            var request = new RestRequest("https://localhost:44386/api/posts/" + id, DataFormat.Json);
            var response = client.Get<PostResponse>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

            var requestComments = new RestRequest("https://localhost:44386/api/comments/", DataFormat.Json);
            var responseComments = client.Get<List<Comments>>(requestComments.AddHeader("Authorization", "Bearer " + KeyValue(key)));

            List<Comments> listaComments = new List<Comments>();

            foreach (var item in responseComments.Data)
            {
                foreach (var item5 in response.Data.Comments)
                {
                    if (item.Id == item5.Id)
                    {
                        listaComments.Add(item);
                    }
                }
            }

            ViewBag.Comments = listaComments;

            return View(response.Data);
        }

        // GET: PostsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var client = new RestClient();
                    var request = new RestRequest("https://localhost:44386/api/posts", DataFormat.Json);
                    var key = HttpContext.Session.GetString("Token");

                    string value = KeyValue(key);
                    var jwt = value;
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(jwt);

                    CancellationToken cancellationToken;

                    var account = _accountRepository.FindByIdAsync(token.Subject, cancellationToken);

                    model.Account = account.Result;

                    request.AddJsonBody(model);
                    var response = client.Post<Post>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

                    return Redirect("/");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: PostsController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var client = new RestClient();
            var key = HttpContext.Session.GetString("Token");

            var request = new RestRequest("https://localhost:44386/api/posts/" + id, DataFormat.Json);
            var response = client.Get<Post>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

            return View(response.Data);
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Post model)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest("https://localhost:44386/api/posts/" + id, DataFormat.Json);
                request.AddJsonBody(model);
                var key = HttpContext.Session.GetString("Token");

                var response = client.Put<Post>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

                return Redirect("/");
            }
            catch
            {
                return View();
            }
        }

        // GET: PostsController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var client = new RestClient();
            var key = HttpContext.Session.GetString("Token");

            var request = new RestRequest("https://localhost:44386/api/posts/" + id, DataFormat.Json);
            var response = client.Get<Post>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

            return View(response.Data);
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest("https://localhost:44386/api/posts/" + id, DataFormat.Json);
                var key = HttpContext.Session.GetString("Token");

                var response = client.Delete<Post>(request.AddHeader("Authorization", "Bearer " + KeyValue(key)));

                return Redirect("/");
            }
            catch
            {
                return View();
            }
        }

        public string KeyValue(string key)
        {
            string[] peloAmorDeDeusFunciona = key.Split(":");
            string[] agrVai = peloAmorDeDeusFunciona[1].Split("\\");
            string[] agrVaiPF = agrVai[0].Split("}");
            string[] agrVaiPF2 = agrVaiPF[0].Split('"');

            return agrVaiPF2[1];
        }
    }
}
