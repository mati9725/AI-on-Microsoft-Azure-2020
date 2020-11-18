using SmartHomeBot.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartHomeBotWpfApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CommandHandler = new CommandHandler();
            SpeechProcessor = new SpeechProcessor();
        }

        private CommandHandler CommandHandler { get; set; }
        private SpeechProcessor SpeechProcessor { get; set; }

        private async void VoiceCommandBtn_Click(object sender, RoutedEventArgs e)
        {
            await ProcessVoiceCommand();
        }

        private async Task ProcessVoiceCommand()
        {
            this.VoiceCommandBtn.IsEnabled = false;
            try
            {
                var toTextResult = await SpeechProcessor.SpeechToText();
                if (!toTextResult.Success)
                {
                    await SpeechProcessor.TextToSpeech(toTextResult.Text);
                    return;
                }

                await CommandHandler.HandleCommand(toTextResult.Text)
                    .ContinueWith(task => { VoiceCommandBtn.IsEnabled = true; });
            }
            catch (Exception ex)
            {
                this.VoiceCommandBtn.IsEnabled = true;
                //Console.WriteLine(ex);
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
