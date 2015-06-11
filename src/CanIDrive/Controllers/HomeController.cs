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

namespace CanIDrive.Controllers
{
    public class HomeController : Controller
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly IConfiguration _configuration;

        public HomeController(TelemetryClient telemetryClient, IConfiguration configuration)
        {
            _telemetryClient = telemetryClient;
            _configuration = configuration;
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

            // TODO - make application id and subscription key config details
            string applicationId = _configuration.Get("Luis:AppId");
            string subscriptionKey = _configuration.Get("Luis:SubscriptionKey");
            string uriFormat = "https://api.projectoxford.ai/luis/v1/application?id={0}&subscription-key={1}&q={2}";
            string uri = string.Format(uriFormat, applicationId, subscriptionKey, model.SpokenText); // TODO - uri encode!

            var client = new HttpClient();

            var response = await client.GetAsync(uri);
            string responseText = await response.Content.ReadAsStringAsync();

            var serializer = JsonSerializer.Create();
            var luisResponse = serializer.Deserialize<LuisResponse>(new JsonTextReader(new StringReader(responseText)));
            var intent = luisResponse.intents[0];

            var resultModel = new ResultModel
            {
                Drunk = intent.intent == "drunkspeech",
                Confidence = intent.score
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


        // TODO move this and the associated api calls to a service
        private class LuisResponse
        {
            public string query { get; set; }
            public Intent[] intents { get; set; }
            public Entity[] entities { get; set; }
        }
        private class Intent
        {
            public string intent { get; set; }
            public double score { get; set; }
        }
        private class Entity
        {

        }
    }
}
