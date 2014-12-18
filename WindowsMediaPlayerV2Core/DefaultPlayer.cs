﻿using System;
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
            _media.LoadedBehavior = MediaState.Play;
        }

        public override void Pause()
        {
            _media.LoadedBehavior = MediaState.Pause;
        }
        public override void Stop()
        {
            _media.LoadedBehavior = MediaState.Stop;
        }
        public override void Dispose()
        {
            _media.LoadedBehavior = MediaState.Close;
        }


    }
}
