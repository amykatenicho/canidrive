using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNet.Mvc;

namespace CanIDrive.Controllers
{
    public class HomeController : Controller
    {
        private readonly TelemetryClient _telemetryClient;

        public HomeController(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }
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
            _telemetryClient.TrackEvent("TestCompleted");
            _telemetryClient.TrackEvent(viewnames[index]);
            return View(viewnames[index]);
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
