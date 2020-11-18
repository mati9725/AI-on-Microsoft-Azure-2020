using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeBot.Common
{
    public class LUProcessor
    {
        public async Task<PredictionResponse> Process(string text)
        {
            var client = new LUISRuntimeClient(new ApiKeyServiceClientCredentials(Config.LuisSubscriptionKey))
            {
                Endpoint = Config.LuisEndPoint
            };

            return await client.Prediction.GetSlotPredictionAsync(
                new Guid(Config.LuisAppId),
                "Production", //"Staging",
                new PredictionRequest(text));
        }
    }
}
