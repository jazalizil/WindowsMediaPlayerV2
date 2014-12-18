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
    /// Logique d'interaction pour MenuControl.xaml
    /// </summary>
    public partial class MenuControl : UserControl
    {
        public MenuControl()
        {
            InitializeComponent();
        }

        private void MenuControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".mp3";
            dlg.Filter = "Multimedia Files (*.mp3, *.wav, *.mp4, *.avi, *.jpg, *.jpeg, *.png)|*.mp3;*.wav;*.mp4;*.avi;*.jpg;*.jpeg;*.png";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                int index = dlg.FileName.LastIndexOf('\\');
                String fNameWithExt = dlg.FileName.Substring(index + 1);
                index = fNameWithExt.IndexOf('.');
                String fName = fNameWithExt.Substring(0, index);
                String ext = fNameWithExt.Substring(index + 1);
                var MPVM = (MediaPlayerVM)this.DataContext;
                MessageBox.Show(fNameWithExt + "-->" + fName + " + " + ext);


                MPVM.FileName = fName;
                MPVM.FileExt = ext;
                if (MPVM.CreateCommand.CanExecute(null))
                    MPVM.CreateCommand.Execute(null);
            }
        }

        private void OpenDir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ImportPlaylist_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SavePlaylist_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

    }
}
