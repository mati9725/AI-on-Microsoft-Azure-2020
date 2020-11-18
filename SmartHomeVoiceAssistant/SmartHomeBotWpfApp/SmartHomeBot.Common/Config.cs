using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHomeBot.Common
{
    public static class Config
    {
        //TODO Fill all properties, to run app properly
        public static string SpeechApiKey { get; } = "";
        public static string SpeechApiRegion { get; } = "";

        public static string LuisAppId { get; } = "";
        public static Guid LuisAppGuid { get; } = new Guid(LuisAppId);
        public static string LuisSubscriptionKey { get; } = "";
        public static string LuisEndPoint{ get; } = "";
    }
}
