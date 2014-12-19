using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WindowsMediaPlayerV2Core
{
    [Export(typeof(IPlayer))]
    public abstract class Player : IPlayer
    {
        protected ObservableCollection<Media> _playList;
        protected Media _current;

        public enum STATUS
        {
            PLAY = 0,
            PAUSE,
            STOP
        }
        public Player()
        {
            this._playList = new ObservableCollection<Media>();
        }
        public ObservableCollection<Media> Playlist
        {
            get { return _playList; }
        }
        public event EventHandler<PlayerArg> PlayerHandler;
        public virtual void Play()
        {
        }
        public virtual void Pause()
        {
        }
        public virtual void Stop()
        {
        }
        public virtual void Dispose() { }

        public virtual Media Create<T>(String path, String name, String ext) where T : Media, new()
        {
            _current = new T();
            Playlist.Add(_current);
            _current.Title = name;
            _current.Extension = ext;
            _current.Source = new Uri(path);
            return _current;
        }
    }
}
