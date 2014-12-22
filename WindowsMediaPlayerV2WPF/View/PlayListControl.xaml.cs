using System.Windows.Controls;

namespace WindowsMediaPlayerV2.View
{
    /// <summary>
    /// Logique d'interaction pour PlayListControl.xaml
    /// </summary>
    public partial class PlayListControl : UserControl
    {
        public PlayListControl()
        {
            InitializeComponent();
        }

        //private void Title_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    var WPVM = DataContext as MediaPlayerVM;
        //    String Content = ((Label)sender).Content as String;
        //    var found = from media in WPVM.Playlist where media.Title == Content select media;
        //    WPVM.ToPlay = found.First();
        //}
    }
}
