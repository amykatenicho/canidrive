using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Framework.ConfigurationModel;
using Newtonsoft.Json;

namespace CanIDrive.Services
{
    public class LuisService
    {
        const string UriFormat = "https://api.projectoxford.ai/luis/v1/application?id={0}&subscription-key={1}&q={2}";
        private readonly string _applicationId;
        private readonly string _subscriptionKey;

        public LuisService(IConfiguration configuration)
        {
            _applicationId = configuration.Get("Luis:AppId");
            _subscriptionKey = configuration.Get("Luis:SubscriptionKey");
        }

        public async Task<LuisSobrietyTestResult> TestSobrietyAsync(string spokenText)
        {
            var client = new HttpClient();

            string uri = string.Format(UriFormat, _applicationId, _subscriptionKey, spokenText); // TODO - uri encode!

            var response = await client.GetAsync(uri);
            string responseText = await response.Content.ReadAsStringAsync();

            var serializer = JsonSerializer.Create();
            var luisResponse = serializer.Deserialize<LuisResponse>(new JsonTextReader(new StringReader(responseText)));
            var intent = luisResponse.intents.OrderByDescending(i => i.score).FirstOrDefault();
            if (intent == null)
            {
                return null;
            }

            var result = new LuisSobrietyTestResult
            {
                Drunk = intent.intent == "drunkspeech",
                Confidence = intent.score
            };
            return result;
        }
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

    public class LuisSobrietyTestResult
    {
        public bool Drunk { get; internal set; }
        public double Confidence { get; internal set; }
    }
}
