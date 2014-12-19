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
        public override void Dispose()
        {
            if (_current != null)
                _current.LoadedBehavior = MediaState.Close;
        }


    }
}
