﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using Id3.Info;

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

        public abstract void Play();
        public abstract void Pause();
        public abstract void Stop();
        public abstract Media Next();
        public abstract Media Previous();

        public void SwitchMedia(Media m)
        {
            var FoundList = from med in _playList where med.Title == m.Title select m;
            _current = FoundList.First();
        }
        public virtual void Dispose()
        {
            foreach (var media in Playlist)
                media.Close();
        }

        public Media CreateMedia<T>(String path, String name, String ext) where T : Media, new()
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
