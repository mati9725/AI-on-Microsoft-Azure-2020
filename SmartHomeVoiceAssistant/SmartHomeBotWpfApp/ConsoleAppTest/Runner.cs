using SmartHomeBot.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    public class Runner
    {
        public Runner()
        {
            commandHandler = new CommandHandler();
            speechProcessor = new SpeechProcessor();
        }

        private CommandHandler commandHandler { get; set; }
        private SpeechProcessor speechProcessor { get; set; }

        public async Task Run()
        {
            try
            {
                await commandHandler.HandleVoiceCommand();
            }
            catch (Exception ex)
            {
                //VoiceCommandBtn.IsEnabled = true;
                Console.WriteLine(ex);
                //MessageBox.Show(ex.Message, "Error");
            }
        }

    }
}
