using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RedeDoVerde.Domain.Post;
using RestSharp;

namespace RedeDoVerde.Web.Controllers
{
    public class PostController : Controller
    {

        // GET: PostsController
        public ActionResult Index()
        {
            var client = new RestClient();
            var key = HttpContext.Session.GetString("Token");

            string[] peloAmorDeDeusFunciona = key.Split(":");
            string[] agrVai = peloAmorDeDeusFunciona[1].Split("\\"); 
            string[] agrVaiPF = agrVai[0].Split("}"); 
            string[] agrVaiPF2 = agrVaiPF[0].Split('"'); 

            var request = new RestRequest("https://localhost:44386/api/posts", DataFormat.Json);
            var response = client.Get<List<Post>>(request.AddHeader("Authorization", "Bearer " + agrVaiPF2[1]));

            return View(response.Data);
        }

        // GET: PostsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                if(ModelState.IsValid)
                {
                    var client = new RestClient();
                    var request = new RestRequest("https://localhost:44386/api/posts", DataFormat.Json);
                    request.AddJsonBody(model);

                    var response = client.Post<Post>(request);

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
            return View();
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

                var response = client.Put<Post>(request);

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
            return View();
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

                var response = client.Delete<Post>(request);

                return Redirect("/");
            }
            catch
            {
                return View();
            }
        }
    }
}
