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
using WindowsMediaPlayerV2.ViewModel;

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
