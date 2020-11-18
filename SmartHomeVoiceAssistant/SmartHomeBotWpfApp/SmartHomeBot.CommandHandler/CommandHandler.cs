using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InMemoryDb.Models;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using SmartHomeBot.Common.Enums;
using Newtonsoft.Json.Linq;
using Intent = SmartHomeBot.Common.Enums.Intent;
using InMemoryDb;
using SmartHomeBot.Common.Exceptions;

namespace SmartHomeBot.Common
{
    public class CommandHandler
    {
        private const double validIntentThreshold = 0.70;
        private const double cancelIntentThreshold = 0.70;

        public CommandHandler()
        {
            LUProcessor = new LUProcessor();
            SpeechProcessor = new SpeechProcessor();
            DbContext = StaticDb.CreateContext();
        }

        private ModelContext DbContext { get; set; }
        private LUProcessor LUProcessor { get; set; }
        private SpeechProcessor SpeechProcessor { get; set; }
        public static bool Logging { get; set; }

        public async Task HandleVoiceCommand()
        {
            var toTextResult = await this.SpeechProcessor.SpeechToText();
            if (!toTextResult.Success)
            {
                await SpeechProcessor.TextToSpeech(toTextResult.Text);
                return;
            }

            PredictionResponse command = await TextToCommand(toTextResult.Text);

            await HandleCommand(command);
        }

        public async Task HandleCommand(PredictionResponse command)
        {
            double? topIntentScore = command.Prediction.Intents.Max(x => x.Value.Score);
            LogIfNecessary(command);

            if (!Enum.TryParse(command.Prediction.TopIntent, out Intent topIntent) || topIntent == Intent.None || topIntentScore < validIntentThreshold)
            {
                string textToSay = $"I dont understand command: {command.Query}";
                await SpeechProcessor.TextToSpeech(textToSay);
                return;
            }

            switch (topIntent)
            {
                case Intent.cancel:
                    await RealizeCancel();
                    break;
                case Intent.set_power_state:
                    await RealizeSetPowerState(command);
                    break;
                case Intent.get_list:
                    await RealizeGetList(command);
                    break;
                case Intent.get_weather:
                    await RealizeGetWeather(command);
                    break;
                case Intent.who_is_at_home:
                    await RealizeWhoIsAtHome(command);
                    break;
                case Intent.check_internet_speed:
                    await RealizeCheckInternetSpeed(command);
                    break;
                default:
                    throw new NotSupportedException($"Intent {topIntent} not supported");
            }
        }

        private async Task RealizeCheckInternetSpeed(PredictionResponse command)
        {
            var rnd = new Random();
            var speed = rnd.NextDouble() * 150 + 20;
            var message = FormattableString.Invariant($"Internet speed is {speed:0.#} megabytes per second");
            await SpeechProcessor.TextToSpeech(message);
        }

        private async Task RealizeWhoIsAtHome(PredictionResponse command)
        {
            var rnd = new Random();
            var people = new[] { "Eva", "Julia", "Sylvia", "Adam", "John", "Barry", "Olaf" };
            var peopleAtHome = new List<string>();
            for (int i = 0; i < people.Length; i++)
            {
                if (rnd.NextDouble() < 0.2)
                    break;
                peopleAtHome.Add(people[rnd.Next(people.Length)]);
            }

            var message = peopleAtHome.Any()
                ? $"There is {peopleAtHome.Count} people at home: {string.Join(", ", peopleAtHome)}"
                : "There is nobody at home";

            await SpeechProcessor.TextToSpeech(message);
        }

        private async Task RealizeGetWeather(PredictionResponse command)
        {
            var rnd = new Random();
            var weather = new[] { "cloudy", "rainy", "sunny", "windy" };
            var temp = rnd.NextDouble() * 30;

            var message = FormattableString.Invariant($"It is {weather[rnd.Next(weather.Length)]} outside. The temperature is {temp:0.#} Celsius degrees.");
            await SpeechProcessor.TextToSpeech(message);
        }

        private static void LogIfNecessary(PredictionResponse command)
        {
            if (Logging)
            {
                double? topIntentScore = command.Prediction.Intents.Max(x => x.Value.Score);
                Console.WriteLine($"Recognized intent {command.Prediction.TopIntent} with score {topIntentScore}. Found entities: {string.Join(", ", command.Prediction.Entities.Select(x => x.Key))}");
            }
        }

        private async Task<PredictionResponse> TextToCommand(string commandText)
        {
            return await LUProcessor.Process(commandText);
        }

        private async Task RealizeGetList(PredictionResponse command)
        {
            var deviceType = await ExtractEntity(command, FirstLevelEntity.DeviceType, required: false);
            var location = await ExtractEntity(command, FirstLevelEntity.Location, required: false);
            var powerState = await ExtractEntity(command, FirstLevelEntity.PowerState, required: false);

            var filterText = "";
            IQueryable<Device> query = DbContext.Device;
            if (deviceType != null)
            {
                query = query.Where(x => x.DeviceTypeId == deviceType.LuisEntityId);
                filterText += " of type " + deviceType.Name;
            }

            if (location != null)
            {
                query = query.Where(x => x.LocationId == location.LuisEntityId);
                filterText += " in " + location.Name;

            }

            if (powerState != null)
            {
                query = query.Where(x => x.PowerStateId == powerState.LuisEntityId);
                filterText += " with state " + powerState.Name;
            }

            var results = query.Select(x => x.Name).ToArray();
            var msg = results.Count() > 0
                ? $"Devices{filterText} are: {string.Join(", ", results)}"
                : $"Not found devices{filterText}";
            await SpeechProcessor.TextToSpeech(msg);
        }

        private async Task RealizeSetPowerState(PredictionResponse command)
        {
            var deviceType = await ExtractEntity(command, FirstLevelEntity.DeviceType, required: true);
            var location = await ExtractEntity(command, FirstLevelEntity.Location, required: true);
            var powerState = await ExtractEntity(command, FirstLevelEntity.PowerState, required: true);

            var device = DbContext.Device.FirstOrDefault(x =>
                x.DeviceTypeId == deviceType.LuisEntityId
                && x.LocationId == location.LuisEntityId);

            if (device == null)
            {
                var msg = $"Not found device of type {deviceType.Name} in {location.Name}.";
                SpeechProcessor.TextToSpeech(msg);
                RealizeCancel();
                return;
            }

            var stateChanged = device.PowerStateId != powerState.LuisEntityId;
            device.PowerStateId = powerState.LuisEntityId;
            DbContext.SaveChanges();

            var messageAfterExecution = stateChanged
                ? $"Device {device.Name} {powerState.Name}"
                : $"Device {device.Name} is already {powerState.Name}";

            await SpeechProcessor.TextToSpeech(messageAfterExecution);
        }

        private async Task<LuisEntity> ExtractEntity(PredictionResponse command, FirstLevelEntity firstLevelEntity, bool required)
        {
            LuisEntity result = null;
            var currentCommand = command;
            while (true)
            {
                if (currentCommand != command)
                    LogIfNecessary(currentCommand);

                await CancelIfRequested(currentCommand);

                var mustAskToClarify = false;
                object nextLevelEntityObj = null;
                if (currentCommand.Prediction.Entities.TryGetValue(firstLevelEntity.ToString(), out nextLevelEntityObj)
                    && nextLevelEntityObj is JArray nextLevelEntitiesContainer
                    && nextLevelEntitiesContainer.Count == 1
                    )
                {
                    var nextLevelEntities = nextLevelEntitiesContainer
                        .Select(x => (x.FirstOrDefault() as JProperty))
                        .Where(x => x != null)
                        .Select(x => x.Name)
                        .Distinct()
                        .ToList();

                    if (nextLevelEntities.Count == 1)
                    {
                        var foundEntity = nextLevelEntities.First();
                        return DbContext.LuisEntity.FirstOrDefault(x => x.Name == foundEntity);
                    }
                    else if (nextLevelEntities.Count > 1)
                    {
                        mustAskToClarify = true;
                    }
                }

                if (required || mustAskToClarify)
                {
                    var question = $"Please specify {firstLevelEntity}";
                    SpeechProcessor.TextToSpeech(question);
                    var userResponse = await SpeechProcessor.SpeechToText();
                    var userText = await SpeechProcessor.HandleSpeachToTextResponse(userResponse);
                    currentCommand = await TextToCommand(userText);
                }
                else
                {
                    return null;
                }

            }

            RealizeCancel();
            return null;
        }

        private async Task CancelIfRequested(PredictionResponse currentCommand)
        {
            if (currentCommand.Prediction.TopIntent == Intent.cancel.ToString()
                && currentCommand.Prediction.Intents.Max(x => x.Value.Score > cancelIntentThreshold))
            {
                await RealizeCancel();
                throw new BotException();
            }
        }

        private async Task RealizeCancel()
        {
            await SpeechProcessor.TextToSpeech("Canceling dialog...");
            throw new CancelIntentException();
        }
    }
}
