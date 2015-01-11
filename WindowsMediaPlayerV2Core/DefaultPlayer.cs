using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.IO;

namespace WindowsMediaPlayerV2Core
{
    [Export(typeof(IPlayer))]
    class DefaultPlayer : Player
    {
        public DefaultPlayer()
        {
        }


        public override void Play()
        {
            if (_current != null)
                _current.LoadedBehavior = MediaState.Play;
        }

        public override void Pause()
        {
            if (_current != null)
                _current.LoadedBehavior = MediaState.Pause;
        }
        public override void Stop()
        {
            if (_current != null)
                _current.LoadedBehavior = MediaState.Stop;
        }
        public override Media Next()
        {
            int nextIndex;

            if (_current == null)
                return null;
            nextIndex = _playList.IndexOf(_current) + 1; 
            _current.LoadedBehavior = MediaState.Stop;
            _current = nextIndex < _playList.Count ? _playList[nextIndex] : _playList[0];
            //_current.LoadedBehavior = MediaState.Play;

            return _current;
        }
        public override Media Previous()
        {
            int prevIndex;

            if (_current == null)
                return null;
            prevIndex = _playList.IndexOf(_current) - 1;
            _current.LoadedBehavior = MediaState.Stop;
            _current = prevIndex >= 0 ? _playList[prevIndex] : _playList[_playList.Count - 1];
            //_current.LoadedBehavior = MediaState.Play;
            return _current;
        }
        public override void Dispose()
        {
            if (_current != null)
                _current.LoadedBehavior = MediaState.Close;
        }


    }
}
