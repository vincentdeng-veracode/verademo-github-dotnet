using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; 
using dotnet.Models;


namespace dotnet.Controllers
{
    public class HomeController : Controller
    {
        private const string COOKIE_NAME = "UserDetails";

        protected bool IsUserLoggedIn()
        {
            return string.IsNullOrEmpty(HttpContext.Session.GetString("username")) == false;
        }

        public IActionResult Login(string ReturnUrl = "")
        {
            //var userDetailsCookie = Request.Cookies[COOKIE_NAME];

            // if (userDetailsCookie == null || userDetailsCookie.Length == 0)
            // {
            //     HttpContext.Session.SetString("username", "");

            //     ViewBag.ReturnUrl = ReturnUrl;
            //     return View();
            // }

            // var unencodedUserDetails = Convert.FromBase64String(userDetailsCookie);

            // CustomSerializeModel deserializedUser;

            // using (MemoryStream memoryStream = new MemoryStream(unencodedUserDetails))
            // {
            //     var binaryFormatter = new BinaryFormatter();

            //     // set memory stream position to starting point
            //     memoryStream.Position = 0;

            //     // Deserializes a stream into an object graph and return as a object.
            //     /* START BAD CODE */
            //     deserializedUser = binaryFormatter.Deserialize(memoryStream) as CustomSerializeModel;
            //     /* END BAD CODE */
            // }

            // HttpContext.Session.SetString("username", deserializedUser.UserName);

            //if (Url.IsLocalUrl(ReturnUrl))  
            // if (string.IsNullOrEmpty(ReturnUrl))
            // {
            //     return RedirectToAction("Index");
            // }

            /* START BAD CODE */
            return Redirect(ReturnUrl);
            /* END BAD CODE */
        }
     

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
