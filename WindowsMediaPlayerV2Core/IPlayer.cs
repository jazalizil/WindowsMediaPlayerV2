using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace WindowsMediaPlayerV2Core
{
    public interface IPlayer : IDisposable
    {
        ObservableCollection<Media> Playlist { get; }
        void Play();
        void Pause();
        void Stop();
        Media Create<T>() where T: Media, new();

    }
}
