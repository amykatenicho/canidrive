using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using CanIDrive.Models.Home;

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
            var model = GetResult();
            return View(model);
        }

        private ResultModel GetResult() // simulate making an assessment :-)
        {
            bool drunk = new Random().Next(2) == 0;
            double confidence = (new Random().NextDouble());

            return new ResultModel
            {
                Drunk = drunk,
                Confidence = confidence
            };
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
