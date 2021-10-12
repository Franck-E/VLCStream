using LibVLCSharp.Shared;
using LibVLCSharp.WPF;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace VLCStream
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : Page
    {
        LibVLC libVLC { get; set; }
        MediaPlayer vlcPlayer;

        public View()
        {     
            Core.Initialize();   
            InitializeComponent();
            VideoView.Loaded += VideoView_Loaded;
        }

        public async void StartPlay(string path)
        {
            using (var media = new Media(libVLC, path, FromType.FromLocation))
            {
                await media.Parse(MediaParseOptions.ParseNetwork);
                VideoView.MediaPlayer.Play(media.SubItems.First());                         
            }                  
        }

        public void StopPlay()
        {
            if (VideoView.MediaPlayer.IsPlaying) { VideoView.MediaPlayer.Stop(); }
        }

        /// <summary>
        /// Pass libVLC options, separated by a comma
        /// </summary>
        /// <param name="repeat"></param>
        /// <returns></returns>
        private string[] MediaOptions()
        {
            string[] mediaOptions;

            mediaOptions = new[] { "--input-repeat=5", "--network-caching=8000", "--directx-use-sysmem", "--avcodec-hw=none" };
            return mediaOptions;
        }

        private void VideoView_Loaded(object sender, RoutedEventArgs e)
        {
            libVLC = new LibVLC(MediaOptions());
            vlcPlayer = new MediaPlayer(libVLC);
            VideoView.MediaPlayer = vlcPlayer;
        }


        private void Page_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            VideoView.MediaPlayer.Dispose();
            libVLC.Dispose();
        }
    }
}
