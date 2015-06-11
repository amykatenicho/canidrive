using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace CanIDrive.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("create-profile")]
        public IActionResult CreateProfile()
        {
            return View();
        }
        [HttpGet("test")]
        public IActionResult Test()
        {
            return View();
        }
        [HttpGet("result")]
        public IActionResult Result()
        {
            int index = new Random().Next(2);
            string[] viewnames = new[] { "Result-Sober", "Result-Drunk" };
            return View(viewnames[index]);
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
