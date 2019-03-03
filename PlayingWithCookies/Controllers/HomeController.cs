using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayingWithCookies.Models;

namespace PlayingWithCookies.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string userName, string password, bool isPersistent)
        {
            if (isPersistent)
            {
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(10)
                };
                Response.Cookies.Append("userName", userName, options);
                Response.Cookies.Append("password", password, options);
            }
            else
            {
                Response.Cookies.Append("userName", userName);
                Response.Cookies.Append("password", password);
            }

            return RedirectToAction(nameof(ReadCookie));
        }

        public IActionResult ReadCookie()
        {
            ViewData["userName"] = Request.Cookies["userName"];
            ViewData["password"] = Request.Cookies["password"];
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
