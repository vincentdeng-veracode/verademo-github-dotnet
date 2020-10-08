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

        protected readonly log4net.ILog logger;
        private const string COOKIE_NAME = "UserDetails";

        protected bool IsUserLoggedIn()
        {
            return string.IsNullOrEmpty(HttpContext.Session.GetString("username")) == false;
        }

        public IActionResult GetLogin(string ReturnUrl = "")
        {
            logger.Info("Login page visited: " + ReturnUrl);

            var userDetailsCookie = Request.Cookies[COOKIE_NAME];

            if (userDetailsCookie == null || userDetailsCookie.Length == 0)
            {
                logger.Info("No user cookie");
                HttpContext.Session.SetString("username", "");

                ViewBag.ReturnUrl = ReturnUrl;
                return View();
            }

            logger.Info("User details were remembered");
            var unencodedUserDetails = Convert.FromBase64String(userDetailsCookie);

            CustomSerializeModel deserializedUser;

            using (MemoryStream memoryStream = new MemoryStream(unencodedUserDetails))
            {
                var binaryFormatter = new BinaryFormatter();

                // set memory stream position to starting point
                memoryStream.Position = 0;

                // Deserializes a stream into an object graph and return as a object.
                /* START BAD CODE */
                deserializedUser = binaryFormatter.Deserialize(memoryStream) as CustomSerializeModel;
                /* END BAD CODE */
                logger.Info("User details were retrieved for user: " + deserializedUser.UserName);
            }

            HttpContext.Session.SetString("username", deserializedUser.UserName);

            //if (Url.IsLocalUrl(ReturnUrl))  
            if (string.IsNullOrEmpty(ReturnUrl))
            {
                return RedirectToAction("Feed", "Blab");
            }

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
