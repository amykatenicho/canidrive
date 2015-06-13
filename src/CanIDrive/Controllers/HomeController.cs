using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNet.Mvc;
using CanIDrive.Models.Home;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Framework.ConfigurationModel;
using CanIDrive.Services;

namespace CanIDrive.Controllers
{
    public class HomeController : Controller
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly LuisService _luisService;

        public HomeController(TelemetryClient telemetryClient, LuisService luisService)
        {
            _telemetryClient = telemetryClient;
            _luisService = luisService;
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
        [HttpPost("test")]
        public async Task<IActionResult> Test(TestModel model)
        {
            // TODO - error handling :-)

            var luisResult = await _luisService.TestSobrietyAsync(model.SpokenText);
            if (luisResult == null)
            {
                return View("NotRecognised");
            }

            var resultModel = new ResultModel
            {
                Drunk = luisResult.Drunk,
                Confidence = luisResult.Confidence
            };

            string resultEventName = resultModel.Drunk ? "Result-drunk" : "Result-sober";
            _telemetryClient.TrackEvent("TestCompleted");
            _telemetryClient.TrackEvent(resultEventName);


            return View("Result", resultModel);
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
