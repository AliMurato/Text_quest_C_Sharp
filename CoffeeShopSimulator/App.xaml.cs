using System;
using System.Windows;
using System.Windows.Media;

namespace CoffeeShopSimulator
{
    public partial class App : Application
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Load and play music
            mediaPlayer.Open(new Uri("audio/Lost_haven_shillings.mp3", UriKind.Relative));
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            mediaPlayer.Volume = 0.5;
            mediaPlayer.Play();

            // Display the main window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            // Repeat the music
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
    }
}
