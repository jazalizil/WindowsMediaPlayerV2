using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;


namespace WindowsMediaPlayerV2Core
{
    public interface IPlayer : IDisposable
    {
        MediaElement MediaEL { set;  }
        ObservableCollection<Media> Playlist { get; }
        void Play();
        void Pause();
        void Stop();
        Media Create<T>(String FilePath, String FileName, String FileExt) where T: Media, new();

    }
}
