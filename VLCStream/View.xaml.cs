using LibVLCSharp.Shared;
using LibVLCSharp.WPF;
using System;
using System.Linq;
using System.Windows.Controls;

namespace VLCStream
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : Page
    {
        public LibVLC libVLC { get; set; }

        public View()
        {     
            Core.Initialize();   
            InitializeComponent();
            libVLC = new LibVLC(MediaOptions()); 
        }

        public async void StartPlay(string path)
        {
            await Dispatcher.InvokeAsync(new Action(async () =>
            {
                using (var media = new Media(libVLC, path, FromType.FromLocation))
                {
                    await media.Parse(MediaParseOptions.ParseNetwork);
                    if (media.SubItems.FirstOrDefault() != null)
                    {
                        var vlcPlayer = new MediaPlayer(media.SubItems.FirstOrDefault());
                        VideoView.MediaPlayer = vlcPlayer;
                        VideoView.MediaPlayer.Play();
                    }
                }                  
            }));
        }

        public void StopPlay()
        {
            if ((VideoView.MediaPlayer != null) && VideoView.MediaPlayer.IsPlaying)
                VideoView.MediaPlayer.Stop();
        }

        /// <summary>
        /// Pass libVLC options, separated by a comma
        /// </summary>
        /// <param name="repeat"></param>
        /// <returns></returns>
        private string[] MediaOptions()
        {
            string[] mediaOptions;

            mediaOptions = new[] { "--input-repeat=5", "--network-caching=10000", "--avcodec-hw=none" };
            return mediaOptions;
        }

        private void Page_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            VideoView.MediaPlayer.Dispose();
            libVLC.Dispose();
        }
    }
}
