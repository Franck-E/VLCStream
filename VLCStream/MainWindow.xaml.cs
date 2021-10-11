using System.Windows;

namespace VLCStream
{
    public partial class MainWindow : Window
    {
        public View Page { get; set; }

        public string URL { get; set; } = "https://youtu.be/wTcNtgA6gHs";

        public MainWindow()
        {
            InitializeComponent();
            Page = new View();
            vlcFrame.Content = Page;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (btn.Content.Equals("Play"))
            {
                Page.StartPlay(URL);
                btn.Content = "Stop";
            }
            else
            {
                Page.StopPlay();
                btn.Content = "Play";
            }
        }
    }
}
