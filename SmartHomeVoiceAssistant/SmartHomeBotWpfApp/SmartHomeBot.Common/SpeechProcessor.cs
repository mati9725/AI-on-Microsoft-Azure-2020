using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using SmartHomeBot.Common.Exceptions;
using SmartHomeBot.Common.Models;

namespace SmartHomeBot.Common
{
    public class SpeechProcessor
    {
        public static bool Logging { get; set; }

        public async Task<SpeechToTextResponse> SpeechToText()
        {
            string text;
            var config = SpeechConfig.FromSubscription(Config.SpeechApiKey, Config.SpeechApiRegion);

            using (var recognizer = new SpeechRecognizer(config))
            {
                Console.WriteLine("Listening...");
                var result = await recognizer.RecognizeOnceAsync();

                var success = false;
                switch (result.Reason)
                {
                    case ResultReason.RecognizedSpeech:
                        text = $"{result.Text}";
                        success = true;
                        break;
                    case ResultReason.NoMatch:
                        text = $"NOMATCH: Speech could not be recognized.";
                        break;
                    case ResultReason.Canceled:
                        var cancellation = CancellationDetails.FromResult(result);
                        text = $"CANCELED: Reason={cancellation.Reason}";

                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                            Console.WriteLine($"CANCELED: Did you update the subscription info?");
                        }
                        break;
                    default:
                        text = "Unknown error! Speech not recognized.";
                        break;
                }

                if (Logging)
                    Console.WriteLine("Speech to text result: " + text);

                return new SpeechToTextResponse
                {
                    Success = success,
                    Text = text,
                };
            }
        }

        public async Task TextToSpeech(string text)
        {
            if (Logging)
                Console.WriteLine("Text to speech processing: " + text);

            var config = SpeechConfig.FromSubscription(Config.SpeechApiKey, Config.SpeechApiRegion);

            using (var synthesizer = new SpeechSynthesizer(config))
            {
                using (var result = await synthesizer.SpeakTextAsync(text))
                {

                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        return;
                    }
                    else
                    {
                        var errorMessage = $"Error occured when trying to say {text}";
                        if (result.Reason == ResultReason.Canceled)
                        {
                            var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                            errorMessage += $"\nCANCELED: Reason={cancellation.Reason}";

                            if (cancellation.Reason == CancellationReason.Error)
                            {
                                errorMessage += $"\nCANCELED: ErrorCode={cancellation.ErrorCode}";
                                errorMessage += $"\nCANCELED: ErrorDetails=[{cancellation.ErrorDetails}]";
                            }
                        }
                        throw new SystemException(errorMessage);
                    }
                }
            }
        }

        public async Task<string> HandleSpeachToTextResponse(SpeechToTextResponse response)
        {
            if (!response.Success)
            {
                TextToSpeech( "Error, please try again");
                var secondResult = await SpeechToText();

                if (!secondResult.Success)
                {
                    await TextToSpeech(secondResult.Text);
                    throw new BotException("Error when processing speech. " + response.Text);
                }
                else
                {
                    return secondResult.Text;
                }
            }
            else
            {
                return response.Text;
            }
        }
    }
}
