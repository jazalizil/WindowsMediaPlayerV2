using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace WindowsMediaPlayerV2.View
{
    public class Track
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Number { get; set; }

    }

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ss");
            WindowState = WindowState.Normal;

        }
        protected override void OnStateChanged(EventArgs e)
        {
            this.Title = "Resized!";
            base.OnStateChanged(e);
        }
        void Update()
        {
            var Grid = this.GetTemplateChild("CenterGrid") as Grid;
            MessageBox.Show(Grid.Name);
            Grid.Height = ActualHeight - 125;
        }
        private void MinimizeButton(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void MaximizeButton(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
            Update();
        }
    }




}
