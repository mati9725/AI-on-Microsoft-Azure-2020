using InMemoryDb;
using SmartHomeBot.Common;
using SmartHomeBot.Common.Exceptions;
using System;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    class Program
    {
        static Program()
        {
            commandHandler = new CommandHandler();
            speechProcessor = new SpeechProcessor();
        }

        private static CommandHandler commandHandler { get; set; }
        private static SpeechProcessor speechProcessor { get; set; }

        static async Task Main(string[] args)
        {
            SpeechProcessor.Logging = true;
            CommandHandler.Logging = true;
            while (true)
            {
                //StaticDb.CreateContext();
                try
                {
                    //var runner = new Runner();
                    //await runner.Run();
                    await commandHandler.HandleVoiceCommand();
                }
                catch (CancelIntentException) 
                { }
                catch (BotException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (!string.IsNullOrEmpty(ex.Message))
                        await speechProcessor.TextToSpeech(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error! " + ex.Message);
                }

                Console.WriteLine();
                Console.WriteLine(StaticDb.ListDevices());
                Console.WriteLine();
                Console.WriteLine("***Press Enter and say next command or type \"exit\" to close aplication.***");
                var input = Console.ReadLine();
                if (input.ToLower() == "exit")
                    break;
            }
        }
    }
}
