using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;

namespace WindowsMediaPlayerV2Core
{
    [Export(typeof(IPlayer))]
    public abstract class Player : IPlayer
    {
        protected ObservableCollection<Media> _playList;
        protected Media _current;

        public MediaState PlayerState
        {
            get
            {
                return _current != null ? _current.LoadedBehavior : MediaState.Stop;
            }
        }
        public Player()
        {
            this._playList = new ObservableCollection<Media>();
        }
        public ObservableCollection<Media> Playlist
        {
            get { return _playList; }
        }
        public virtual void Play()
        {
        }
        public virtual void Pause()
        {
        }
        public virtual void Stop()
        {
        }
        public virtual void Dispose()
        {
            foreach (var media in Playlist)
                media.Close();
        }

        public virtual Media CreateMedia<T>(String path, String name, String ext) where T : Media, new()
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
